using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;

namespace Corporate.Plataforms.AzureConfiguration.Client
{
    public class ApplicationConfig
    {
        private static IConfiguration _configuration = null;
        private static IConfigurationRefresher _refresher = null;

        public ApplicationConfig()
        {
            var builder = new ConfigurationBuilder();
            builder.AddAzureAppConfiguration(options =>
            {
                options.Connect(Environment.GetEnvironmentVariable("ConnectionString"))
                        .ConfigureRefresh(refresh =>
                        {
                            refresh.Register("TestApp:Settings:Message")
                                .SetCacheExpiration(TimeSpan.FromSeconds(10));
                        });

                _refresher = options.GetRefresher();
            });

            _configuration = builder.Build();
            PrintMessage().Wait();
        }


        private static async Task PrintMessage()
        {
            Console.WriteLine(_configuration["TestApp:Settings:Message"] ?? "Hello world!");

            // Wait for the user to press Enter
            Console.ReadLine();

            await _refresher.TryRefreshAsync();
            Console.WriteLine(_configuration["TestApp:Settings:Message"] ?? "Hello world!");
        }
    }
}
