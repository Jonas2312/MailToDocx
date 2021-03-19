using System;
using System.Threading;
using Utils;

namespace Converter
{
    public class Pipeline
    {

        public static string name = String.Empty;
        public static void Invoke(string pdf_path, string image_path, string docx_path, string hostname, int port, string username, string password)
        {
            Tuple<string, string> tuple = RecieveMails.Invoke(pdf_path, hostname, port, username, password);

            string name = tuple.Item1;
            string content = tuple.Item2;

            Console.WriteLine(content);

            var v = DictionaryFileConverter.fromString(content);


            PdfToImage.Invoke(name, pdf_path, image_path);
            CropImage.Invoke(name, image_path);
            //ImageToDoc.Invoke(name, image_path, docx_path);
            DictionaryToDocx.Invoke(v, docx_path, name);
        }
    }
}
