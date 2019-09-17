using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doMain.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public class ZipUtils
    {
        public static byte[] Zip(string content, string filename)
        {
            using (ZipFile zip = new ZipFile())
            {
                var memStream = new MemoryStream();
                var streamWriter = new StreamWriter(memStream);

                streamWriter.WriteLine(content);

                streamWriter.Flush();
                memStream.Seek(0, SeekOrigin.Begin);

                ZipEntry e = zip.AddEntry(filename, memStream);
               
                var ms = new MemoryStream();
                ms.Seek(0, SeekOrigin.Begin);

                zip.Save(ms);

                ms.Position = 0;
                byte[] data = ms.ToArray();
                //To write byte array on system
                //File.WriteAllBytes(zipFileName, data);

                return data;
            }
        }
        //public static byte[] Zip(string filename, byte[] bytes)
        //{
            
        //    var memOrigen = new MemoryStream(bytes);
        //    var memDestino = new MemoryStream();
        //    byte[] resultado = null;

        //    using (var fileZip = new ZipFile($"{filename}.zip"))
        //    {
        //        fileZip.AddEntry($"{filename}.xml", memOrigen);
        //        fileZip.Save(memDestino);
        //        resultado = memDestino.ToArray();
        //    }
        //    // Liberamos memoria RAM.
        //    memOrigen.Close();
        //    memDestino.Close();

        //    return resultado;
        //}

        /// <summary>
        /// Uns the zip.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="path">The path.</param>
        public static void UnZip(string fileName,string path)
        {
            using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile(fileName))
            {
                zip.ExtractAll(path);
            }
        }
    }
}
