using Microsoft.Extensions.Configuration;
using MongoConfiguration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoConfigurationTester
{
    public class TestApp
    {
        private readonly IConfiguration _configuration;

        public TestApp(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Run()
        {
            var a = 1;
            var configuration = MongoConfigurationHelpers.DeserializeObject<MyConfiguration>(_configuration["mykeyvalue"]);
        }
    }
}
