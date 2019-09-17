using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace doMain.Utils
{
    public static class ImageUtils
    {

        const string BITMAP_ID_BLOCK = "BM";
        const string JPG_ID_BLOCK = "\u00FF\u00D8\u00FF";
        const string PNG_ID_BLOCK = "\u0089PNG\r\n\u001a\n";
        const string GIF_ID_BLOCK = "GIF8";
        const string TIFF_ID_BLOCK = "II*\u0000";
        const int DEFAULT_OLEHEADERSIZE = 78;

        public static byte[] ConvertOleObjectToByteArray(object content)
        {
            if (content != null && !(content is DBNull))
            {
                byte[] oleFieldBytes = (byte[])content;
                byte[] imageBytes = null;
                // Get a UTF7 Encoded string version
                Encoding u8 = Encoding.UTF7;
                string strTemp = u8.GetString(oleFieldBytes);
                // Get the first 300 characters from the string
                string strVTemp = strTemp.Substring(0, 300);
                // Search for the block
                int iPos = -1;
                if (strVTemp.IndexOf(BITMAP_ID_BLOCK) != -1)
                {
                    iPos = strVTemp.IndexOf(BITMAP_ID_BLOCK);
                }
                else if (strVTemp.IndexOf(JPG_ID_BLOCK) != -1)
                {
                    iPos = strVTemp.IndexOf(JPG_ID_BLOCK);
                }
                else if (strVTemp.IndexOf(PNG_ID_BLOCK) != -1)
                {
                    iPos = strVTemp.IndexOf(PNG_ID_BLOCK);
                }
                else if (strVTemp.IndexOf(GIF_ID_BLOCK) != -1)
                {
                    iPos = strVTemp.IndexOf(GIF_ID_BLOCK);
                }
                else if (strVTemp.IndexOf(TIFF_ID_BLOCK) != -1)
                {
                    iPos = strVTemp.IndexOf(TIFF_ID_BLOCK);
                }
                // From the position above get the new image
                if (iPos == -1)
                {
                    iPos = DEFAULT_OLEHEADERSIZE;
                }
                imageBytes = new byte[oleFieldBytes.LongLength - iPos];
                MemoryStream ms = new MemoryStream();
                ms.Write(oleFieldBytes, iPos, oleFieldBytes.Length - iPos);
                imageBytes = ms.ToArray();
                ms.Close();
                ms.Dispose();
                return imageBytes;
            }
            return null;
        }

        public static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            if (byteArrayIn == null)
                return null;

            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        public static Bitmap ConvertTextToImage(string txt, string fontname, int fontsize, Color bgcolor, Color fcolor, int width, int Height)
        {
            Bitmap bmp = new Bitmap(width, Height);
            using (Graphics graphics = Graphics.FromImage(bmp))
            {

                Font font = new Font(fontname, fontsize);
                graphics.FillRectangle(new SolidBrush(bgcolor), 0, 0, bmp.Width, bmp.Height);
                graphics.DrawString(txt, font, new SolidBrush(fcolor), 0, 0);
                graphics.Flush();
                font.Dispose();
                graphics.Dispose();


            }
            return bmp;
        }

        public static byte[] StringToBytes(string inputString)
        {
            byte[] imageBytes = Encoding.Unicode.GetBytes(inputString);

            // Don't need to use the constructor that takes the starting offset and length
            // as we're using the whole byte array.
            //MemoryStream ms = new MemoryStream(imageBytes);

            //Image image = Image.FromStream(ms, true, true);

            //string text = inputString;
            //Bitmap bitmap = new Bitmap(1, 1);
            //Font font = new Font("Arial", 25, FontStyle.Regular, GraphicsUnit.Pixel);
            //Graphics graphics = Graphics.FromImage(bitmap);
            //int width = (int)graphics.MeasureString(text, font).Width;
            //int height = (int)graphics.MeasureString(text, font).Height;
            //bitmap = new Bitmap(bitmap, new Size(width, height));
            //graphics = Graphics.FromImage(bitmap);
            //graphics.Clear(Color.White);
            //graphics.SmoothingMode = SmoothingMode.AntiAlias;
            //graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            //graphics.DrawString(text, font, new SolidBrush(Color.Black), 0, 0);
            //graphics.Flush();
            //graphics.Dispose();
            //string fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ".jpg";
            //bitmap.Save(@"C:\Software\" + fileName, ImageFormat.Jpeg);




            return imageBytes;
        }

        public static byte[] ImageToByteArray(System.Drawing.Image imageIn, System.Drawing.Imaging.ImageFormat format)
        {

            if (imageIn == null)
                return null;

            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, format);
            return ms.ToArray();
        }

        /// <summary>
        /// Images to base64.
        /// </summary>
        /// <param name="imageIn">The image in.</param>
        /// <param name="format">The format.</param>
        /// <returns></returns>
        public static string ImageToBase64(System.Drawing.Image imageIn, System.Drawing.Imaging.ImageFormat format)
        {

            if (imageIn == null)
                return null;

            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, format);
            byte[] byteImage = ms.ToArray();
            return Convert.ToBase64String(byteImage); //Get Base64
        }

        public static bool CompareBitmaps(Image left, Image right)
        {
            if (object.Equals(left, right))
                return true;
            if (left == null || right == null)
                return false;

            //if (!left.Size.Equals(right.Size) || !left.PixelFormat.Equals(right.PixelFormat))
            //    return false;

            Bitmap leftBitmap = left as Bitmap;
            Bitmap rightBitmap = right as Bitmap;
            if (leftBitmap == null || rightBitmap == null)
                return true;

            #region Optimized code for performance

            int bytes = left.Width * left.Height * (Image.GetPixelFormatSize(left.PixelFormat) / 8);

            bool result = true;
            byte[] b1bytes = new byte[bytes];
            byte[] b2bytes = new byte[bytes];

            BitmapData bmd1 = leftBitmap.LockBits(new Rectangle(0, 0, leftBitmap.Width - 1, leftBitmap.Height - 1), ImageLockMode.ReadOnly, leftBitmap.PixelFormat);
            BitmapData bmd2 = rightBitmap.LockBits(new Rectangle(0, 0, rightBitmap.Width - 1, rightBitmap.Height - 1), ImageLockMode.ReadOnly, rightBitmap.PixelFormat);

            Marshal.Copy(bmd1.Scan0, b1bytes, 0, bytes);
            Marshal.Copy(bmd2.Scan0, b2bytes, 0, bytes);

            for (int n = 0; n <= bytes - 1; n++)
            {
                if (b1bytes[n] != b2bytes[n])
                {
                    result = false;
                    break;
                }
            }

            leftBitmap.UnlockBits(bmd1);
            rightBitmap.UnlockBits(bmd2);

            #endregion

            return result;
        }
    }
}
