using System;
using System.Collections.Generic;

//using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Web;

namespace doMain.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public class AssemblyUtils
    {

      

        public static string ExecuteFolder()
        {
            try
            {
                return HttpContext.Current.Server.MapPath("~");
            }
            catch
            {
                return FileUtils.GetFolder(Assembly.GetEntryAssembly().Location);
            }
        }

        public static string VersionExe()
        {
            try
            {
                var versionInfo = FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetEntryAssembly().Location);
                return versionInfo.ProductVersion;
            }
            catch
            {
                return "";
            }
        }

        public static string Version(Assembly assembly)
        {
            string version  = assembly.FullName.Split(',')[1].Trim().Split('=')[1].Trim();
            //"SmartContact, Version=1.0.0.52, Culture=neutral, PublicKeyToken=null"
            return version;
        }

        public static Form GetForm(string assemblyfile,string namespaceform,params object[] pars)
        {

            string dllpath = FileUtils.GetFolder(Application.ExecutablePath) + "\\" + assemblyfile + ".dll";

            System.Reflection.Assembly myDllAssembly =
                System.Reflection.Assembly.LoadFile(dllpath);
                Form MyDLLFormInstance = (Form)myDllAssembly.CreateInstance(namespaceform,
                                    false, //do not ignore the case
                                    BindingFlags.CreateInstance, //specifies we want to call a ctor method
                                    null, //a null binder specifies the default binder will be used (works in most cases)
                                    pars, //the arguments (based on the arguments,
                                    //CreateInstance() will decide which ctor to invoke)
                                    null, //CultureInfo is null so we will use 
                                    //the culture info from the current thread
                                    null //no activation attributes
                                    );
            return MyDLLFormInstance;
        }

       

      
    }
}
