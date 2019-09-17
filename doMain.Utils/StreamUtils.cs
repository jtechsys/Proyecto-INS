using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace doMain.Utils
{
    public class StreamUtils
    {
        public static Stream StringToStream(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static byte[] StreamToBytes(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }

        public static Stream BytesToStream(byte[] b)
        {
            if (b == null)
                return null;

            Stream stream = new MemoryStream(b);
            return stream;
        }

        public static MemoryStream BitmapToStream(Bitmap b)
        {
            
            ImageConverter ic = new ImageConverter();
            Byte[] ba = (Byte[])ic.ConvertTo(b, typeof(Byte[]));
            MemoryStream logo = new MemoryStream(ba);
            return logo;
        }
    }
}
