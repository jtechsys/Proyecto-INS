using System;

using System.IO;
using System.Collections.Generic;
using System.Text;

using System.Collections;
using System.Net;
using System.IO.IsolatedStorage;
using System.IO.Compression;
using System.Windows.Forms;
using System.Diagnostics;
using System.Linq;
using System.Configuration;

namespace doMain.Utils
{
    public sealed class FileUtils
    {
        //public static string PathFolder = "";

        public static byte[] ConvertFileToByte(string path)
        {
            return System.IO.File.ReadAllBytes(path);
        }

        public static void ConvertByteToFile(string path,byte[] arrbytes)
        {
            File.WriteAllBytes(path, arrbytes);
        } 

        public static string GetFirstFile(string pathFolder)
        {
           
                if (pathFolder == "")
                    return null;

                DirectoryInfo info = new DirectoryInfo(pathFolder);
                FileInfo[] files = info.GetFiles().OrderBy(p => p.CreationTime).ToArray();
                foreach (FileInfo file in files)
                {
                  

                    return file.Name;
                }
           

            return null;
        }

      

        public static string GetFirstFile(string pathFolder, string contains)
        {
            
                if (pathFolder == "")
                    return null;

                DirectoryInfo info = new DirectoryInfo(pathFolder);
                FileInfo[] files = info.GetFiles().OrderBy(p => p.CreationTime).Where(x => x.Name.Contains(contains)).ToArray();
                foreach (FileInfo file in files)
                {                  
                    return file.Name;
                }
            
            return null;
        }


        public static void WaitExistImage(string pathFolder)
        {
            var fi = new System.IO.DirectoryInfo(pathFolder);
            int seconds = 0;
            if (fi.Exists)
            {

                fi.Refresh();
                while (fi.GetFiles().Count() == 0)
                {
                    System.Threading.Thread.Sleep(1000);

                    fi.Refresh();

                    seconds++;
                    if (seconds == 60)
                        return;

                }
            }
        }

        public static string[] List(string folder, string searchPattern, bool includeSubFolders)
        {
            try
            {
                if (includeSubFolders)
                    return Directory.GetFiles(folder, searchPattern, System.IO.SearchOption.AllDirectories);
                else
                    return Directory.GetFiles(folder, searchPattern, System.IO.SearchOption.TopDirectoryOnly);
            }
            catch
            {
                return null;
            }
        }

        public static void Copy(string sourcePath, string targetPath, string[] extensions)
        {
           
            var files = (from file in Directory.EnumerateFiles(sourcePath)
                         where extensions.Contains(Path.GetExtension(file), StringComparer.InvariantCultureIgnoreCase) // comment this out if you don't want to filter extensions
                         select new
                         {
                             Source = file,
                             Destination = Path.Combine(targetPath, Path.GetFileName(file))
                         });

            foreach (var file in files)
            {
                File.Copy(file.Source, file.Destination,true);
            }
        }

        public static void Copy(string sourceFile, string destFile)
        {
            System.IO.File.Copy(sourceFile, destFile, true);
        }
    

        public static string Zip(string text)
        {

            byte[] buffer = Encoding.UTF8.GetBytes(text);
            MemoryStream ms = new MemoryStream();
            using (GZipStream zip = new GZipStream(ms, CompressionMode.Compress, true))
            {
                zip.Write(buffer, 0, buffer.Length);
            }
            ms.Position = 0;
            MemoryStream outStream = new MemoryStream();
            byte[] compressed = new byte[ms.Length];
            ms.Read(compressed, 0, compressed.Length);
            byte[] gzBuffer = new byte[compressed.Length + 4];
            System.Buffer.BlockCopy(compressed, 0, gzBuffer, 4, compressed.Length);
            System.Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gzBuffer, 0, 4);
            return Convert.ToBase64String(gzBuffer);
        }

        public static string UnZip(string compressedText)
        {
            byte[] gzBuffer = Convert.FromBase64String(compressedText);
            using (MemoryStream ms = new MemoryStream())
            {
                int msgLength = BitConverter.ToInt32(gzBuffer, 0);
                ms.Write(gzBuffer, 4, gzBuffer.Length - 4);
                byte[] buffer = new byte[msgLength];
                ms.Position = 0;

                using (GZipStream zip = new GZipStream(ms, CompressionMode.Decompress))
                {
                    zip.Read(buffer, 0, buffer.Length);
                }
                return Encoding.UTF8.GetString(buffer);
            }
        }


        /// <summary>
        /// Archivos inaccesibles donde se guarda informacion 
        /// </summary>
        /// <param name="path">ruta relativa</param>
        /// <param name="text">texto</param>
        public static void IsolatedFileCreate(string path, string text)
        {
            IsolatedStorageFile almacen = IsolatedStorageFile.GetMachineStoreForAssembly();
            IsolatedStorageFileStream archivo = new IsolatedStorageFileStream(path, System.IO.FileMode.OpenOrCreate, almacen);
            StreamWriter sw = new StreamWriter(archivo);
            sw.Write(text);
            sw.Close();
            archivo.Close();

            //Data Compartement es el sitio donde se alamcena el archivo
        }

        /// <summary>
        /// Archivos inaccesibles donde se guarda informacion 
        /// </summary>
        public static string IsolatedFileRead(string path)
        {
            IsolatedStorageFile almacen = IsolatedStorageFile.GetMachineStoreForAssembly();
            IsolatedStorageFileStream archivo = new IsolatedStorageFileStream(path, System.IO.FileMode.OpenOrCreate, almacen);
            StreamReader sw = new StreamReader(archivo);

            return sw.ReadLine();
        }

        public static string pathFolder()
        {
            FolderBrowserDialog dialogo = new FolderBrowserDialog();
            dialogo.ShowNewFolderButton = false;

            if (dialogo.ShowDialog() == DialogResult.OK)
            {
                return dialogo.SelectedPath;
            }
            else
            {
                return "";
            }
        }

       
        public static bool Exist(string filpath)
        {
            string rootpath = Path.GetDirectoryName(filpath);


            if (File.Exists(filpath))
                return true;

            return false;
        }

        public static bool ExistAppSetting(string filpath)
        {
            //string rootpath = Path.GetDirectoryName(filpath);            
            string rootpath = ConfigurationManager.AppSettings["RutaCrypto"];
            
            if (File.Exists(filpath))
                return true;

            //if (rootpath == "")
            //    return false;

            //foreach (string subDir in Directory.GetDirectories(rootpath, "*", SearchOption.AllDirectories))
            //{
            //    if (File.Exists(Path.Combine(subDir, filpath)))
            //        return true;
            //}

            return false;
        }


        public static void Open(string filepath)
        {
            System.Diagnostics.Process.Start(filepath);
        }

        public static void CreateFile(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            fs.Close();
        }

        public static void WriteText(string filename, string text)
        {
            try
            {
                if (!Exist(filename))
                {
                    FileStream fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                    fs.Close();
                }


                TextWriter tw = new StreamWriter(filename);
                tw.WriteLine(text);
                tw.Close();

            } catch
            {

            }
        }

        public static void WriteText(string text)
        {
            WriteText(AssemblyUtils.ExecuteFolder(), text);
        }

        public static void ReplaceText(string filename, string oldtext, string newtext)
        {
            if (!Exist(filename))
            {
                FileStream fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                fs.Close();
            }

            string text = ReadText(filename);
            text = text.Replace(oldtext, newtext);
            TextWriter tw = new StreamWriter(filename);
            tw.WriteLine(text);
            tw.Close();
        }

        /// <summary>
        /// Abre un archivo y muestra su contenido como texto. 
        /// </summary>
        /// <param name="filename">ruta del archivo</param>
        /// <returns></returns>
        public static string ReadText(string filename)
        {

            if (!Exist(filename))
                return "";

            if (filename == "")
                return "";

            StringBuilder sb = new StringBuilder();
            StreamReader re = File.OpenText(filename);
            string input = null;
            while ((input = re.ReadLine()) != null)
            {
                sb.AppendLine(input);
            }
            re.Close();

            return sb.ToString();
        }



        //public static void DeleteFiles(string directorio)
        //{
        //    string[] carpeta = Directory.GetFiles(directorio);
        //    for (int i = 0; i < carpeta.Length; i++)
        //    {
        //        File.Delete(carpeta[i].ToString());
        //    }
        //}


        //public static void Delete(string pathFolder, string contains)
        //{

        //    if (pathFolder == "")
        //        return;

        //    DirectoryInfo info = new DirectoryInfo(pathFolder);
        //    FileInfo[] files = info.GetFiles().OrderBy(p => p.CreationTime).Where(x => x.Name.Contains(contains)).ToArray();
        //    foreach (FileInfo file in files)
        //    {
        //        Delete(file.Name,true);
        //    }


        //}

        public static void Delete(string folder, string searchPattern)
        {
            var files = List(folder,searchPattern, false);
            foreach (var f in files)
            {
                Delete(f, false);
            }

        }

        public static void Delete(string file,bool wait = false)
        {
            //File.Delete(file);
            var fi = new FileInfo(file);
            if (fi.Exists)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
                //fi.Delete();
                if (wait)
                {
                    fi.Refresh();
                    while (fi.Exists)
                    {
                        System.Threading.Thread.Sleep(100);
                        fi.Refresh();
                    }
                }
            }

            //return true;
        }

        /// <summary>
        /// Gets the file name without extension.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public static string GetFileNameWithoutExtension(string path)
        {
            return System.IO.Path.GetFileNameWithoutExtension(path);
        }

        /// <summary>
        /// Files the name extension.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public static string GetFileNameExtension(string path)
        {
            return System.IO.Path.GetFileName(path);
        }

        public static string GetFolder(string path)
        {
            return System.IO.Path.GetDirectoryName(path) + @"\";
        }

        public static string GetExtension(string path)
        {
            return System.IO.Path.GetExtension(path);
        }

        public static bool Move(string rutaOrigen, string carpetaFinal, string nuevoNombre, string extension)
        {
            carpetaFinal = carpetaFinal + @"\" + nuevoNombre + extension;
            File.Move(rutaOrigen, carpetaFinal);
            return true;
        }

        public static bool Move(string rutaOrigen, string rutaFinal)
        {
            bool estado = false;
            do
            {
               
                    if (File.Exists(rutaFinal))
                        File.Delete(rutaFinal);

                    File.Move(rutaOrigen, rutaFinal);
                    estado = true;
                //}
                //catch
                //{
                //    estado = false;
                //}
            }
            while (estado == false);
            return true;
        }

        //public static bool CopiarImagen(string rutaOrigen, string rutaFinal)
        //{
        //    if (File.Exists(rutaFinal))
        //        File.Delete(rutaFinal);
        //    File.Copy(rutaOrigen, rutaFinal);
        //    return true;
        //}


        public static void CopyBlocking(string originalpath, string copypath)
        {
            //bool ready = false;

            FileInfo original = new FileInfo(originalpath);
            FileInfo copyPath = new FileInfo(copypath);


            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Path = copyPath.Directory.FullName;
            watcher.Filter = "*" + copyPath.Extension;
            watcher.EnableRaisingEvents = true;

            bool fileReady = false;
            bool firsttime = true;
            DateTime previousLastWriteTime = new DateTime();

            // modify this as you think you need to...
            int waitTimeMs = 100;

            watcher.Changed += (sender, e) =>
            {
                // Get the time the file was modified
                // Check it again in 100 ms
                // When it has gone a while without modification, it's done.
                while (!fileReady)
                {
                    // We need to initialize for the "first time", 
                    // ie. when the file was just created.
                    // (Really, this could probably be initialized off the
                    // time of the copy now that I'm thinking of it.)
                    if (firsttime)
                    {
                        previousLastWriteTime = System.IO.File.GetLastWriteTime(copyPath.FullName);
                        firsttime = false;
                        System.Threading.Thread.Sleep(waitTimeMs);
                        continue;
                    }

                    DateTime currentLastWriteTime = System.IO.File.GetLastWriteTime(copyPath.FullName);

                    bool fileModified = (currentLastWriteTime != previousLastWriteTime);

                    if (fileModified)
                    {
                        previousLastWriteTime = currentLastWriteTime;
                        System.Threading.Thread.Sleep(waitTimeMs);
                        continue;
                    }
                    else
                    {
                        fileReady = true;
                        break;
                    }
                }
            };

            System.IO.File.Copy(original.FullName, copyPath.FullName, true);

            // This guy here chills out until the filesystemwatcher 
            // tells him the file isn't being writen to anymore.
            while (!fileReady)
            {
                System.Threading.Thread.Sleep(waitTimeMs);
            }
        }

        public static string[] SpliWords(string nombreFile)
        {
            string cadenadelimitador = ".#";
            char[] delimitador = cadenadelimitador.ToCharArray();
            string[] split = null;

            split = nombreFile.Split(delimitador);

            return split;
        }

        public static string[] SpliWords(string word, string limitador)
        {
            string cadenadelimitador = limitador;
            char[] delimitador = cadenadelimitador.ToCharArray();
            string[] split = null;

            split = word.Split(delimitador);

            return split;
        }


        public static bool StateFile(string file)
        {

            bool estado = false;
            do
            {
               
                    estado = true;
                

            }
            while (estado == true);

            return estado;

        }


        public static string RenameFile(string filename, string newname)
        {

            string pathoriginal = Path.GetDirectoryName(filename);
            return pathoriginal + Constantes.SeparadorPath + newname;
        }

        public static string RenombrarFile(string folder, string nombre , string reemplazo)
        {

            string newname = "";
            if (nombre == reemplazo)
            {
                newname = folder + Constantes.SeparadorPath + reemplazo;
                return newname;
            }

            try
            {
                //DirectoryInfo di = new DirectoryInfo(folder);
                //string destino = "";
                //foreach (FileInfo fi in di.GetFiles())
                //{
                //    if (fi.Name.ToLower().IndexOf(nombre) > -1)
                //    {
                //        destino = fi.DirectoryName + "\\" +
                //                  fi.Name.ToLower().Replace(nombre, reemplazo);
                //        File.Move(fi.FullName, destino);

                //    }
                //}

                string origen = folder + Constantes.SeparadorPath + nombre;
                string destino = folder + Constantes.SeparadorPath + reemplazo;
                File.Move(origen, destino);

                WaitFileExist(destino);

                newname = destino;

            }
            catch
            {
                newname = "";
            }

            return newname;
        }


        public static void WaitFileExist(string filename)
        {
            bool estado = false;
            do
            {
                try
                {
                    if (FileUtils.Exist(filename))
                    {
                        estado = true;
                    }
                }
                catch
                {
                    estado = false;
                }

            }
            while (estado == false);
        }

        /// <summary>
        /// Retorna los archivos de una carpeta
        /// </summary>
        /// <param name="path">c:/</param>
        /// <param name="searchPattern">*.jpg;*.bmp;*.gif;*.tif</param>
        /// <param name="includeSubFolders">false</param>
        /// <returns></returns>
        public static string[] FindFiles(string path, string searchPattern, bool includeSubFolders)
        {

            if (!FolderUtils.Exist(path))
            {
                return null;
            }

            char[] chrArray1 = new char[] { ';' };
            string[] textArray1 = searchPattern.Split(chrArray1);
            ArrayList list1 = new ArrayList();
            for (int num1 = 0; num1 < textArray1.Length; num1++)
            {
                string[] textArray2 = Directory.GetFiles(path, textArray1[num1].ToString(), SearchOption.AllDirectories);
                list1.AddRange(textArray2);
            }
            return (string[])list1.ToArray((Type)typeof(string));
        }


        public static ArrayList FindServerFiles(string path, string searchPattern, bool includeSubFolders)
        {

            ArrayList files = new ArrayList();

            System.Net.WebClient Client = new WebClient();
            Stream strm = Client.OpenRead(path);
            StreamReader sr = new StreamReader(strm);
            string line;
            do
            {
                line = sr.ReadLine();
                files.Add(line);
            }
            while (line != null);
            strm.Close();

            return files;

        }

        public static string[] Find(string basepath,string filename)
        {
            return Directory.GetFiles(basepath, filename, SearchOption.AllDirectories);
        }

        public static bool Exist(string basepath, string filename)
        {
            var files = Directory.GetFiles(basepath, filename, SearchOption.AllDirectories);
            if (files.Length > 0)
                return true;
            else
                return false;
        }

    }


}
