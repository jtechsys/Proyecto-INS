using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web;

namespace doMain.Utils
{
    public sealed class FolderUtils
    {
        public static string[] List(string path)
        {
            var directories = Directory.GetDirectories(path);
            return directories;
        }

        public static void Delete(string path)
        {
            System.IO.Directory.Delete(path, true);
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="folderName"></param>
        /// <param name="basePath"></param>
        /// <param name="deleteIfExists"></param>
        public static void CreateDirectory(string folderName, string basePath = null)
        {
            if (basePath == null)
            {
                try
                {
                    basePath = HttpRuntime.AppDomainAppPath;
                }
                catch
                {
                    
                }
            }

            string path = "";

            if (basePath != null)
                path = Path.Combine(basePath, folderName);
            else
                path = folderName;

            //if (!Directory.Exists(name))
            //{
            //    name =  + @"\" + name;
            //}


            Directory.CreateDirectory(path);
        }


        public static void CreateFoldersPath(string path)
        {
            //var path = @"C:\Foo\Bar";
            new System.IO.DirectoryInfo(path).Create();
        }

        public static void Open(string path)
        {
            string myPath = path;
            System.Diagnostics.Process prc = new System.Diagnostics.Process();
            prc.StartInfo.FileName = myPath;
            prc.Start();
        }

        public static bool Exist(string path)
        {

            return Directory.Exists(path);
        }
    }
}
