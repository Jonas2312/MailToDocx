using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MailToDocx
{
    public class Pipeline
    {

        public static string name = String.Empty;
        public static void f(string pdf_path, string image_path, string docx_path, string hostname, int port, string username, string password)
        {
            while (true)
            {
                name = String.Empty;
                try
                {
                    name = RecieveMails.f(pdf_path, name, hostname, port, username, password);
                    if (!(name.Equals(String.Empty)) && !name.Contains("ERROR"))
                    {
                        name = PdfToImage.f(name, pdf_path, image_path);
                        if (!name.Contains("ERROR"))
                        {
                            name = CropImage.f(name, image_path);
                            if (!name.Contains("ERROR"))
                            {
                                name = ImageToDocx.f(name, image_path, docx_path);
                            }
                        }
                    }
                }
                catch(Exception e)
                {
                    name = "ERROR: Something went wrong: " + e.Message;
                }

                Thread.Sleep(2000);
                Console.WriteLine("Looking for mails....");
            }
        }
    }
}
