
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace MailToDocx
{
    class ImageToDocx
    {
        public static string f(string name, string image_path, string docx_path)
        {
            try
            {
                var imageFile = image_path + "\\" + name + "_cropped.jpg";
                string docFile = docx_path + "\\" + name + ".docx";

                using (DocX document = DocX.Create(docFile))
                {
                    // Add an image into the document.    
                    Image image = document.AddImage(imageFile);

                    // Create a picture (A custom view of an Image).
                    Picture picture = image.CreatePicture();

                    picture.HeightInches = 5.6f;
                    picture.WidthInches = 6.5f;

                    // Insert a new Paragraph into the document.
                    Paragraph p1 = document.InsertParagraph();

                    // Append content to the Paragraph
                    p1.AppendPicture(picture);

                    // Save this document.
                    document.Save();
                }
            }
            catch
            {
                name = "ERROR: Could not convert Image to docx.";
            }
            return name;
            


        }

    }
}

