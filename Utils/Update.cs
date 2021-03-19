using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Utils
{
    public class Update
    {
        const string configFile = "config.json";
        const string newConfigFile = "new-config.json";
        const string dataFolder = "Data";
        const string zipFile = "Data.zip";


        Dictionary<string, string> Dictionary;
        public Update(string standardConfigFileLink, string standardPackageDownloadLink)
        {
            try
            {
                Dictionary = DictionaryFileConverter.fromFile<string>(configFile);
            }
            catch
            {
                CreateNewDictionary(standardConfigFileLink, standardPackageDownloadLink);
                DictionaryFileConverter.toFile(Dictionary, configFile);
            }
        }

        public void CreateNewDictionary(string standardConfigFileLink, string standardPackageDownloadLink)
        {
            Dictionary = new Dictionary<string, string>();
            Dictionary.Add("Version", "1.0");
            Dictionary.Add("NewConfigFileLink", standardConfigFileLink);
            Dictionary.Add("PackageDownloadLink", standardPackageDownloadLink);
            Dictionary.Add("ApplicationFilePath", "Release\\Frontend.exe");
        }


        public bool CheckForUpdate()
        {
            using (var client = new WebClient())
            {
                try
                {
                    client.DownloadFile(Dictionary["NewConfigFileLink"], newConfigFile);
                }
                catch(Exception e)
                {
                    File.Delete(newConfigFile);
                    throw e;
                }
            }

            Dictionary<string, string> newDictionary = new Dictionary<string, string>();
            newDictionary = DictionaryFileConverter.fromFile<string>(newConfigFile);

            return !newDictionary["Version"].Equals(Dictionary["Version"]);
        }

        public void PerformUpdate(bool perform)
        {
            if(perform)
            {

                if (Directory.Exists(dataFolder))
                    Directory.Delete(dataFolder, true);
                File.Delete(configFile);

                while(File.Exists(configFile))
                {
                    Thread.Sleep(100);
                }

                System.IO.File.Move(newConfigFile, configFile);

                Dictionary = DictionaryFileConverter.fromFile<string>(configFile);
                using (var client = new WebClient())
                {
                    try
                    {
                        client.DownloadFile(Dictionary["PackageDownloadLink"], zipFile);
                    }
                    catch (Exception e)
                    {
                        File.Delete(zipFile);
                        throw e;
                    }
                    
                }
                ZipFile.ExtractToDirectory(zipFile, dataFolder);
                File.Delete(zipFile);
            }
            else
            {
                File.Delete(newConfigFile);
            }
        }

        public void StartApplication()
        {
            string applicationPath = dataFolder + "\\" + Dictionary["ApplicationFilePath"];
            Process.Start(applicationPath);
        }
    }
}
