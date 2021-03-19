using System;
using System.Diagnostics;
using Utils;

namespace Application
{
    class Program
    {        
        static string standardConfigFileLink = "https://github.com/Jonas2312/MailToDocxUpdate/releases/latest/download/config.json";
        static string standardPackageDownloadLink = "https://github.com/Jonas2312/MailToDocxUpdate/releases/latest/download/Release.zip";
        static void Main(string[] args)
        {
            Update Update = new Update(standardConfigFileLink, standardPackageDownloadLink);

            try
            {
                bool needUpdate = Update.CheckForUpdate();
                if (needUpdate)
                {
                    Console.WriteLine("Downloading new Update...");
                    Update.PerformUpdate(needUpdate);
                    Console.WriteLine("Finished installing new Update.");
                }
                else
                {
                    Console.WriteLine("No new Update.");
                    Update.PerformUpdate(needUpdate);
                }
            }
            catch
            {

            }

            try
            {
                Update.StartApplication();
            }
            catch(Exception e)
            {
                Console.WriteLine("Could not start Application. Error was " + e.Message);
                Update.CheckForUpdate();
                Update.PerformUpdate(true);
                Update.StartApplication();
            }
            Console.WriteLine("Press a key to exit.");
            Console.ReadKey();
        }
    }
}
