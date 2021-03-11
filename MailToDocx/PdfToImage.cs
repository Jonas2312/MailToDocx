using Pdf2Image;

namespace MailToDocx
{
    class PdfToImage
    {
        public static string f(string name, string pdf_path, string image_path)
        {
            string PdfFile = pdf_path + "\\" + name + ".pdf";

            //List<System.Drawing.Image> images = PdfSplitter.GetImages(PdfFile, PdfSplitter.Scale.High);

            try
            {
                PdfSplitter.WriteImages(PdfFile, image_path, PdfSplitter.Scale.VeryHigh, PdfSplitter.CompressionLevel.None);                
            }
            catch
            {
                name = "ERROR: Could not convert PDF to image.";
            }
            return name;
        }
    }
}
