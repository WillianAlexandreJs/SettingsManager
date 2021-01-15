using Corporate.Plataforms.AzureConfiguration.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corporate.Plataforms.AzureConfiguration.ConsoleTest
{
    public class TesteApp
    {
        public string Setting1 { get; set; }
        public string Setting2 { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            TesteApp testeApp = new TesteApp();

            ApplicationConfig<TesteApp> app = new ApplicationConfig<TesteApp>(testeApp);


            Console.ReadLine();
        }
    }
}
