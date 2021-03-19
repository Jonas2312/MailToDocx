using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utils;

namespace Frontend.Model
{
    class UserData
    {
        public string ID;

        public string PdfFolder;
        public string JpgFolder;
        public string DocxFolder;

        public string Hostname;
        public int Port;
        public string Username;
        public string Password;

        const string FileName = "UserData.txt";
        public UserData()
        {
            Load();
        }

        public void Save()
        {
            Dictionary<string, string> Dictionary = new Dictionary<string, string>();
            Dictionary.Add("PdfFolder", PdfFolder);
            Dictionary.Add("JpgFolder", JpgFolder);
            Dictionary.Add("DocxFolder", DocxFolder);

            Dictionary.Add("Hostname", Hostname);
            Dictionary.Add("Port", Port.ToString());
            Dictionary.Add("Username", Username);
            Dictionary.Add("Password", Password);

            DictionaryFileConverter.toFile(Dictionary, FileName);
        }

        public void Load()
        {
            if (!File.Exists(FileName))
            {
                FileStream file = File.Create(FileName);
                file.Close();
            }
            else
            {
                try
                {
                    Dictionary<string, string> Dictionary = DictionaryFileConverter.fromFile<string>(FileName);
                    if (Dictionary == null)
                        throw new Exception();

                    PdfFolder = Dictionary["PdfFolder"];
                    JpgFolder = Dictionary["JpgFolder"];
                    DocxFolder = Dictionary["DocxFolder"];

                    Hostname = Dictionary["Hostname"];
                    Port = Int32.Parse(Dictionary["Port"]);
                    Username = Dictionary["Username"];
                    Password = Dictionary["Password"];
                }
                catch
                {
                    File.Delete(FileName);
                }
            }
        }
    }
}
