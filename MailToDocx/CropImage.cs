using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailToDocx
{
    class CropImage
    {
        public static string f(string name, string image_path)
        {
            try
            {   
                Bitmap source = new Bitmap(image_path + "\\" + name + "_1.jpg");
                Rectangle section = new Rectangle(new Point(0, 800), new Size(3000, 3000));

                Bitmap CroppedImage = CropBitmap(source, section);
                CroppedImage.Save(image_path + "\\" + name + "_cropped.jpg");
            }
            catch
            {
                name = "ERROR: Could not crop image.";
            }

            return name;
        }

        public static Bitmap CropBitmap(Bitmap source, Rectangle section)
        {
            var bitmap = new Bitmap(section.Width, section.Height);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.DrawImage(source, 0, 0, section, GraphicsUnit.Pixel);
                return bitmap;
            }
        }
    }
}
