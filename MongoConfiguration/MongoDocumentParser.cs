using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using Newtonsoft.Json.Schema;

namespace MongoConfiguration
{
    // Inspired by JsonConfigurationFileParser
    public class MongoDocumentParser
    {
        private readonly IDictionary<string, string> _data = new SortedDictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        private readonly Stack<string> _context = new Stack<string>();
        private string _currentPath;

        public static IDictionary<string, string> Parse(BsonDocument input)
            => new MongoDocumentParser().ParseStream(input);

        private IDictionary<string, string> ParseStream(BsonDocument input)
        {
            _data.Clear();

            foreach (var element in input.Elements)
            {
                EnterContext(element.Name);
                VisitValue(element.Value);
                ExitContext();
            }

            return _data;
        }

        private void VisitValue(BsonValue value)
        {
            if (value.IsObjectId)
            {
                // Ignoring object id
                return;
            }
            if (value.IsBsonArray)
            {
                var index = 0;
                var arrayValues = value.AsBsonArray.Values;

                foreach (var arrayElement in arrayValues)
                {
                    EnterContext(index.ToString());
                    VisitValue(arrayElement);
                    ExitContext();
                    index++;
                }
            }
            else if (value.IsBsonDocument)
            {
                var bsonDocument = value.AsBsonDocument;
                foreach (BsonElement element in bsonDocument.Elements)
                {
                    EnterContext(element.Name);
                    VisitValue(element.Value);
                    ExitContext();
                }
            }
            else if (value.IsBoolean || value.IsNumeric || value.IsBsonNull
                     || value.IsString)
            {
                var key = _currentPath;
                if (_data.ContainsKey(key))
                {
                    throw new FormatException($"Duplicate key found: [{key}]");
                }
                _data[key] = value.ToString();
            }
            else
            {
                throw new FormatException($"Unsupported type [{value}]");
            }
        }

        private void EnterContext(string context)
        {
            _context.Push(context);
            _currentPath = ConfigurationPath.Combine(_context.Reverse());
        }

        private void ExitContext()
        {
            _context.Pop();
            _currentPath = ConfigurationPath.Combine(_context.Reverse());
        }
    }
}
