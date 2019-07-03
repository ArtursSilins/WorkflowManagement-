using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ProgressPicture
{
    public class Picture
    {
        private Graphics graphic;
        public byte[] Create(float startLinePosition, float endLinePosition)
        {
            Bitmap bitmap = new Bitmap(100, 21);
            graphic = Graphics.FromImage(bitmap);
            graphic.FillRectangle(Brushes.White, 0, 0, 100, 21);
            graphic.FillRectangle(Brushes.Green, startLinePosition, 0, endLinePosition, 21);
            MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);

            byte[] pic = stream.ToArray();

            return pic;
        }
        public byte[] CreateWithProcentage(float procentage)
        {
            Bitmap bitmap = new Bitmap(100, 21);
            graphic = Graphics.FromImage(bitmap);
            graphic.FillRectangle(Brushes.White, 0, 0, 100, 21);
            graphic.FillRectangle(Brushes.Green, 0, 0, procentage, 21);
            Font myFont = new Font("Bold", 12);
            graphic.DrawString(procentage.ToString() + " %", myFont, Brushes.Black, 40, 0);
            MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);

            byte[] pic = stream.ToArray();

            return pic;
        }
        public byte[] CreatePictureComplited()
        {
            Bitmap bitmap = new Bitmap(100, 21);
            graphic = Graphics.FromImage(bitmap);
            graphic.FillRectangle(Brushes.White, 0, 0, 100, 21);
            graphic.FillRectangle(Brushes.Green, 0, 0, 0, 21);
            Font myFont = new Font("Bold", 10);
            graphic.DrawString("Complite", myFont, Brushes.Black, 10, 0);
            MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);

            byte[] pic = stream.ToArray();

            return pic;
        }
        public byte[] CreatePictureWaiting()
        {
            Bitmap bitmap = new Bitmap(100, 21);
            graphic = Graphics.FromImage(bitmap);
            graphic.FillRectangle(Brushes.White, 0, 0, 100, 21);
            graphic.FillRectangle(Brushes.Green, 0, 0, 0, 21);
            Font myFont = new Font("Bold", 10);
            graphic.DrawString("   Waiting", myFont, Brushes.Black, 10, 0);
            MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);

            byte[] pic = stream.ToArray();

            return pic;
        }
        public byte[] CreatePictureCompleted()
        {
            Bitmap bitmap = new Bitmap(100, 21);
            graphic = Graphics.FromImage(bitmap);
            graphic.FillRectangle(Brushes.White, 0, 0, 100, 21);
            graphic.FillRectangle(Brushes.Green, 0, 0, 0, 21);
            Font myFont = new Font("Bold", 10);
            graphic.DrawString("  Completed", myFont, Brushes.Black, 10, 0);
            MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);

            byte[] pic = stream.ToArray();

            return pic;
        }
    }
}
