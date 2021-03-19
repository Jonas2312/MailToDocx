using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using System;
using System.Drawing;

namespace Converter
{
    class ImageToDoc
    {
        public static void Invoke(string name, string image_path, string doc_path)
        {
            var imageFile = image_path + "\\" + name + "_cropped.jpg";
            string docFile = doc_path + "\\" + name + ".doc";

            //Create Document  
            Document document = new Document();
            Section s = document.AddSection();
            Paragraph p = s.AddParagraph();

            //Insert Image and Set Its Size  
            DocPicture Pic = p.AppendPicture(Image.FromFile(imageFile));
            Pic.Width = 475;
            Pic.Height = 475;

            //Save and Launch  
            document.SaveToFile(docFile, FileFormat.Doc);
            document.Close();
        }
        
    }
}
