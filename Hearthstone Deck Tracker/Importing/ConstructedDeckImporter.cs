using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Hearthstone_Deck_Tracker.Hearthstone;
using System.Drawing.Imaging;
using System.IO;

namespace Hearthstone_Deck_Tracker.Importing
{
    public static class ConstructedDeckImporter
    {
        //location of the decklist
        private static readonly Point DecklistLocation = new Point(964,79);

        private static readonly Point CutoutLocation = new Point(1100, 86);
        //DecklistCutoutRectangle
        private static readonly Size CutoutSize = new Size(15, 589);

        private static readonly int CoutoutsHight = 18;

        public static Deck Import(Bitmap bmp)
        {
            var cardCutouts = ExtractCutouts(bmp);

            var cutoutTemplates = ExtractTemplates();

            return null;
        }

        private static Dictionary<string, Bitmap> ExtractTemplates()
        {
            var templates = new Dictionary<string, Bitmap>();

            foreach(var file in Directory.GetFiles(Path.GetFullPath("../../../Hearthstone Deck Tracker/Images/Bars")))
            {
                var templateRect = new Rectangle(85, 5, 20, 24);
                var cutout = new Bitmap(CutoutSize.Width, CoutoutsHight,PixelFormat.Format24bppRgb);

                using (var g = Graphics.FromImage(cutout))
                    g.DrawImage(new Bitmap(file), new Rectangle(0, 0, cutout.Width, cutout.Height), templateRect, GraphicsUnit.Pixel);

                if (Path.GetFileNameWithoutExtension(file) == "CS2_146")
                {
                    //print example
                }

                templates.Add(Path.GetFileNameWithoutExtension(file), cutout);
            }

            return templates;
        }

        private static List<Bitmap> ExtractCutouts(Bitmap original)
        {
            var cutouts = new List<Bitmap>();

            Bitmap bmp = ResizeImage(original);

            bmp = CropRect(bmp, new Rectangle(CutoutLocation,CutoutSize));

            for (int i = 1; i < CutoutSize.Height; i += (CoutoutsHight + 11))
            {
                var rect = new Rectangle(0, i, bmp.Width, CoutoutsHight);
                cutouts.Add(CropRect(bmp, rect));

                //distance is not exactly 11
                if (i % 5 == 0)
                {
                    i -= 1;
                }

            }

            int a = 0;
            foreach(var item in cutouts)
            {
                item.Save(@"D:\Code\HearthStoneDeckTracker\img_" + a + ".bmp");
                a++;
            }

            return cutouts;
        }

        // Resize captured image to a height of 768
        private static Bitmap ResizeImage(Bitmap original)
        {
            const int height = 768;

            if (original.Height == height)
                return original;

            const double ratio = 4.0 / 3.0;
            var width = Convert.ToInt32(height * ratio);
            var cropWidth = Convert.ToInt32(original.Height * ratio);
            const int posX = 0;

            var scaled = new Bitmap(width, height);
            using (var g = Graphics.FromImage(scaled))
                g.DrawImage(original, new Rectangle(0, 0, width, height), new Rectangle(posX, 0, cropWidth, original.Height), GraphicsUnit.Pixel);

            return scaled;
        }

        private static Bitmap CropRect(Bitmap bmp, Rectangle rect)
        {
            var target = new Bitmap(rect.Width, rect.Height, PixelFormat.Format24bppRgb);
            using (var g = Graphics.FromImage(target))
                g.DrawImage(bmp, new Rectangle(0, 0, target.Width, target.Height), rect, GraphicsUnit.Pixel);
            return target;
        }
    }
}
