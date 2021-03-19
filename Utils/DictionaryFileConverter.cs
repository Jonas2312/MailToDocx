using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public static class DictionaryFileConverter
    {
        public static string toString(Dictionary<string, string> dictionary)
        {
            string text = String.Empty;
            foreach (KeyValuePair<string, string> keyValuePair in dictionary)
            {
                text += keyValuePair.Key + ":   " + keyValuePair.Value + "\n";
            }
            return text;
        }

        public static Dictionary<string, string> fromString(string text)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            string[] parts = text.Split(';');
            foreach(string part in parts)
            {
                if (part.Length == 0)
                    continue;

                string trimmedPart = part.Substring(0, part.Length);
                string[] KeyAndValue = trimmedPart.Split(':');

                if(KeyAndValue.Length == 2)
                    dictionary.Add(KeyAndValue[0], KeyAndValue[1]);
            }

            return dictionary;
        }

        public static void toFile<T>(Dictionary<T,T> dictionary, string path)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(dictionary));
        }

        public static Dictionary<T,T> fromFile<T>(string path)
        {
            Dictionary<T, T> dictionary = JsonConvert.DeserializeObject<Dictionary<T,T>>(File.ReadAllText(path));
            return dictionary;
        }
    }
}
