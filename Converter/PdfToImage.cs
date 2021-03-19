using Pdf2Image;

namespace Converter
{
    class PdfToImage
    {
        public static void Invoke(string name, string pdf_path, string image_path)
        {
            string PdfFile = pdf_path + "\\" + name + ".pdf";

            //List<System.Drawing.Image> images = PdfSplitter.GetImages(PdfFile, PdfSplitter.Scale.High);

            PdfSplitter.WriteImages(PdfFile, image_path, PdfSplitter.Scale.VeryHigh, PdfSplitter.CompressionLevel.None);
            
        }
    }
}
