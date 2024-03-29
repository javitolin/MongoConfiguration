# MongoConfiguration
> An IConfiguration implementation for receiving configuration from a MongoDB database

[![Build Status](https://dev.azure.com/asadodevculture/AsadoDevCultureProjects/_apis/build/status/MongoConfiguration-Build?branchName=master)](https://dev.azure.com/asadodevculture/AsadoDevCultureProjects/_build/latest?definitionId=1&branchName=master)

## Installation
```
Install-Package MongoConfiguration
```

## Usage example
1. Create a `MongoConfigurationSettings` object with needed configuration
2. Use the extension method `AddMongoProvider`
3. Use IConfiguration in a constructor and use like you would use with any other provider

```
            MongoConfigurationSettings mongoConfigurationSettings = new MongoConfigurationSettings
            {
                ConnectionString = "mongodb+srv://test_user:test_user@cluster0.sn9q9.azure.mongodb.net/test",
                DatabaseName = "configuration_db",
                CollectionName = "configuration_collection",
                KeyName = "MyKey",
                KeyValue = "mykeyvalue"
            };

            var builder = new ConfigurationBuilder()
                .AddMongoProvider(mongoConfigurationSettings);
```

## Contributing
1. Fork it (<https://github.com/javitolin/MongoConfiguration/fork>)
2. Create your feature branch (`git checkout -b feature/fooBar`)
3. Commit your changes (`git commit -am 'Add some fooBar'`)
4. Push to the branch (`git push origin feature/fooBar`)
5. Create a new Pull Request

## Meta

[AsadoDevCulture](https://AsadoDevCulture.com) 

[@jdorfsman](https://twitter.com/jdorfsman)

Distributed under the MIT license.
