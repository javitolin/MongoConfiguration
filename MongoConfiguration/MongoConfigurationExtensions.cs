using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Text;
using DnsClient.Protocol;
using MongoDB.Bson.Serialization;

namespace MongoConfiguration
{
    public static class MongoConfigurationExtensions
    {
        public static IConfigurationBuilder AddMongoProvider(this IConfigurationBuilder configuration,
            MongoConfigurationSettings mongoConfigurationSettings, bool optional = false)
        {
            configuration.AddMongoProvider(settings =>
            {
                settings.KeyValue = mongoConfigurationSettings.KeyValue;
                settings.KeyName = mongoConfigurationSettings.KeyName;
                settings.DatabaseName = mongoConfigurationSettings.DatabaseName;
                settings.CollectionName = mongoConfigurationSettings.CollectionName;
                settings.ConnectionString = mongoConfigurationSettings.ConnectionString;
            },
                optional);

            return configuration;
        }

        public static IConfigurationBuilder AddMongoProvider(this IConfigurationBuilder configuration,
            Action<MongoConfigurationSettings> options, bool optional = false)
        {
            _ = options ?? throw new ArgumentNullException(nameof(options));
            var configurationOptions = new MongoConfigurationSettings();
            options(configurationOptions);
            configuration.Add(new MongoConfigurationSource(configurationOptions, optional));
            return configuration;
        }

        public static T BindFromBsonElement<T>(this IConfigurationSection section)
        {
            StringBuilder jsonStringBuilder = new StringBuilder("{");
            jsonStringBuilder = ConvertSectionsToJsonString(section, jsonStringBuilder);
            jsonStringBuilder.Append("}");
            return BsonSerializer.Deserialize<T>(jsonStringBuilder.ToString());
        }

        private static StringBuilder ConvertSectionsToJsonString(IConfigurationSection section, StringBuilder jsonString)
        {
            foreach (var child in section.GetChildren())
            {
                if (child.Value == null)
                {
                    bool isArray = int.TryParse(child.GetChildren().FirstOrDefault()?.Key, out var _);
                    jsonString.Append($"\"{child.Key}\" : ");
                    jsonString.Append(isArray ? "[" : "{");
                    jsonString = ConvertSectionsToJsonString(child, jsonString);
                    jsonString.Append(isArray ? "], " : "}, ");
                }
                else
                {
                    if (int.TryParse(child.Key, out int _))
                    {
                        jsonString.Append($"\"{child.Value}\", ");
                    }
                    else
                    {
                        jsonString.Append($"\"{child.Key}\" : \"{child.Value}\", ");
                    }
                }
            }

            return jsonString;
        }
    }
}
