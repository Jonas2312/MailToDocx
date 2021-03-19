using System;
using System.Collections.Generic;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using System;
using System.Drawing;
using Utils;

namespace Converter
{
    class DictionaryToDocx
    {
        public static void Invoke(Dictionary<string, string> dictionary, string docx_path, string name)
        {
            string docFile = docx_path + "\\" + name + ".docx";

            //Create Document  
            Document document = new Document();
            Section s = document.AddSection();
            Paragraph p = s.AddParagraph();

            //Insert Image and Set Its Size  
            foreach (KeyValuePair<string, string> keyValuePair in dictionary)
            {
                string text = String.Empty;

                text += keyValuePair.Key + ":  \n";
                var v = p.AppendText(text);
                v.CharacterFormat.Bold = true;

                text = keyValuePair.Value + "\n";
                v = p.AppendText(text);
            }
            
            

            //Save and Launch  
            document.SaveToFile(docFile, FileFormat.Docx2013);
            document.Close();
        }
    }
}
