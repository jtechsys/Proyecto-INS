using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doMain.Utils
{
    public class ProcessUtils
    {
        public static void Run(string fileName,string arguments)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = fileName;
            startInfo.Arguments = arguments;
            Process.Start(startInfo);
        }
        public static bool IsAlreadyRunning(string name)
        {
            var a = Process.GetProcesses();

            // get all processes by Current Process name
            Process[] processes =
                Process.GetProcessesByName(
                    name);

            // if there is more than one process...
            if (processes.Length > 0)
            {
                // if other process id is OUR process ID...
                // then the other process is at index 1
                // otherwise other process is at index 0
                int n = (processes[0].Id == Process.GetCurrentProcess().Id) ? 1 : 0;

                // get the window handle
                IntPtr hWnd = processes[n].MainWindowHandle;

                // if iconic, we need to restore the window
                //if (IsIconic(hWnd)) ShowWindowAsync(hWnd, SW_RESTORE);

                //// Bring it to the foreground
                //SetForegroundWindow(hWnd);
                return true;
            }
            return false;
        }


    }
}
