using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using System;
using System.Threading.Tasks;

namespace Corporate.Plataforms.AzureConfiguration.Client
{

    public class ApplicationConfig<T>
    {
        private static IConfiguration _configuration = null;
        private static IConfigurationRefresher _refresher = null;

        public ApplicationConfig(T section)
        {
            _configuration = new ConfigurationBuilder()
            .AddAzureAppConfiguration(options =>
            {
                options.Connect("Endpoint=https://willappconfiguration.azconfig.io;Id=d9NT-le-s0:+orCiChlZOihEcAltWYt;Secret=/zodEns9xWs7Mejy3V0+SV05AJJNHPz7R4sBT0vRtFo=")
                .TrimKeyPrefix("TesteApp")
                        .ConfigureRefresh(refresh =>
                        {
                            refresh.Register(key: "TesteApp:*", refreshAll: true).SetCacheExpiration(TimeSpan.FromSeconds(10));
                        });

                _refresher = options.GetRefresher();
            }).Build();

            PrintMessage(section).Wait();
        }


        private static async Task PrintMessage(T section)
        {
            Console.WriteLine(_configuration["XPApplication"] ?? "Empty");
            var teste = _configuration.GetSection(typeof(T).Name);

            // Wait for the user to press Enter
              Console.ReadLine();

            await _refresher.TryRefreshAsync();
            Console.WriteLine(_configuration["XPApplication"] ?? "Empty");
        }
    }
}
