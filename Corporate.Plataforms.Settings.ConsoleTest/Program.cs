﻿using Corporate.Plataforms.Settings.Client;
using Newtonsoft.Json;
using System;
using System.Reflection;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Application: {args[0]}   Instance: {args[1]}");
            ApplicationConfigHub<ApplicationSettings> applicationConfigHub = new ApplicationConfigHub<ApplicationSettings>(ApplicationSettings.Instance);
            applicationConfigHub.StartHubConnection("http://localhost:38102", "SettingsHub", args[0], args[1], new TimeSpan(0, 0, 5), itemConfig =>
            {
                PropertyInfo propertyInfo = applicationConfigHub._configuration.GetType().GetProperty(itemConfig.Nome);
                if (propertyInfo != null)
                    propertyInfo.SetValue(applicationConfigHub._configuration, Convert.ChangeType(itemConfig.Valor, propertyInfo.PropertyType), null);
                else
                    Console.WriteLine($"Propriedade {itemConfig.Nome} não encontrada");

                Console.WriteLine($"Date: { DateTime.Now:dd/MM/yyyy HH:mm:ss}, Item: { JsonConvert.SerializeObject(ApplicationSettings.Instance)}");
            });

            Console.ReadLine();
            
        }
    }
}