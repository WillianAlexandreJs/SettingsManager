using System;

namespace Corporate.Plataforms.Settings.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(120, 10);

            if (args.Length < 2)
            {
                args = new string[] { "Nd.Notification", "Notify_1" };
            }

            Console.WriteLine($"Application: {args[0]}   Instance: {args[1]}");

            SettingManager.Instance.StartConnectionSettingsManager(args[0], args[1]);
            Console.ReadLine();

        }
    }
}
