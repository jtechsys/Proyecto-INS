using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace doMain.Utils
{

    public struct Rect
    {

        public int Left
        {
            get;
            set;
        }
        public int Top
        {
            get;
            set;
        }


        public int Right
        {
            get;
            set;
        }
        public int Bottom
        {
            get;
            set;
        }

    }

    public class WinApiUtils
    {
        const int MAXTITLE = 255;

        private static Dictionary<IntPtr, string> mTitlesList;



        private delegate bool EnumDelegate(IntPtr hWnd, int lParam);

        [DllImport("user32.dll", EntryPoint = "EnumDesktopWindows",
         ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool _EnumDesktopWindows(IntPtr hDesktop,
        EnumDelegate lpEnumCallbackFunction, IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "GetWindowText",
         ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int _GetWindowText(IntPtr hWnd,
        StringBuilder lpWindowText, int nMaxCount);

        [DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        public static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);


        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(int hWnd, int msg, int wParam, IntPtr lParam);


        [DllImport("User32.dll")]
        public static extern Int32 FindWindow(String lpClassName, String lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);

        private const int BN_CLICKED = 245;
        [DllImport("user32.dll")]
        static extern IntPtr GetDlgItem(IntPtr hWnd, int nIDDlgItem);

        [DllImport("user32.dll")]
        static extern bool ClientToScreen(IntPtr hWnd, ref Point lpPoint);
        const int WM_COMMAND = 0x0111;
        //const int BN_CLICKED = 0;
        const int ButtonId = 0x00;

        public static void ClickButton(String lpWindowName,string buttonname)
        {
            var hwnd = FindWindow(null, lpWindowName);
            if (hwnd != 0)
            {
                //IntPtr hWndButton = GetDlgItem((IntPtr)hwnd, ButtonId);                
                //int wParam = (BN_CLICKED << 16) | (ButtonId & 0xffff);
                //SendMessage(hwnd, WM_COMMAND, wParam, hWndButton);

                SetForegroundWindow((IntPtr)hwnd);
                SendKeys.SendWait("{ENTER}");
            }
        }



        // Activate an application window.
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);


        //public static void ClickButton(IntPtr handle)
        //{


        //    //IntPtr hwndChild = FindWindowEx(hwnd, IntPtr.Zero, "Button", title);
        //    //SendMessage((int)hwndChild, BN_CLICKED, 0, IntPtr.Zero);

        //    //WINDOWINFO wi = new WINDOWINFO(false);
        //    //GetWindowInfo(p[0].MainWindowHandle, ref wi);

        //    //SendLeftClick((wi.rcWindow.left + wi.rcWindow.right) / 2, (wi.rcWindow.top + wi.rcWindow.bottom) / 2);

        //    //int calculatorHandle = handle.ToInt32(); // FindWindow(null, "Calculadora");

        //    //// Verify that Calculator is a running process.
        //    //if (calculatorHandle == 0)
        //    //{
        //    //    //MessageBox.Show("Calculator is not running.");
        //    //    return;
        //    //}

        //    //// Make Calculator the foreground application and send it 
        //    //// a set of calculations.
        //    SetForegroundWindow(handle);

        //    //SendKeys.SendWait("111");
        //    //SendKeys.SendWait("*");
        //    //SendKeys.SendWait("11");
        //    //SendKeys.SendWait("=");

        //    //SendMessage((int)thisWindow, 006303C8, 0, IntPtr.Zero);

        //}

        private static bool EnumWindowsProc2(IntPtr hWnd, int lParam)
        {
            string title = GetWindowText(hWnd);
            mTitlesList.Add(hWnd, title);
            return true;
        }

        /// <summary>
        /// Returns the caption of a windows by given HWND identifier.
        /// </summary>
        public static string GetWindowText(IntPtr hWnd)
        {
            StringBuilder title = new StringBuilder(MAXTITLE);
            int titleLength = _GetWindowText(hWnd, title, title.Capacity + 1);
            title.Length = titleLength;

            return title.ToString();
        }

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);
        [DllImport("USER32.DLL")]
        private static extern IntPtr GetShellWindow();


        public static long GetActiveWindowHandler()
        {
            const int nChars = 256;


            IntPtr handle = IntPtr.Zero;
            StringBuilder Buff = new StringBuilder(nChars);
            handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {

                //if (Buff.ToString() != this.Name)
                //{
                    //selecthandle = handle;
                    return handle.ToInt64();
                //}
                //else
                //    return "";
            }
            return 0;
        }

        public static string GetActiveWindowTitle()
        {
            const int nChars = 256;


            IntPtr handle = IntPtr.Zero;
            StringBuilder Buff = new StringBuilder(nChars);
            handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
  
                return Buff.ToString();
               
            }
            return null;
        }

        public static bool IsActiveWindow(IntPtr handle)
        {
             
            if (handle == GetForegroundWindow())
            {
                return true;
            }

            return false;
            
        }

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetWindowPos(IntPtr hWnd,
           int hWndInsertAfter, int x, int y, int cx, int cy, int uFlags);



        [DllImport("USER32.DLL")]
 
static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);


        [DllImport("USER32.DLL")]
        private static extern bool IsWindowVisible(IntPtr hWnd);
        [DllImport("USER32.DLL")]
        private static extern bool EnumWindows(EnumWindowsProc enumFunc, int lParam);
        private delegate bool EnumWindowsProc(IntPtr hWnd, int lParam);
        [DllImport("USER32.DLL")]
        private static extern int GetWindowTextLength(IntPtr hWnd);
        [DllImport("USER32.dll")]
        private static extern bool MoveWindow(IntPtr hwnd, int x, int y, int cx, int cy, bool repaint);

        private const int HWND_TOPMOST = -1;
        private const int HWND_NOTOPMOST = -2;
        private const int SWP_NOMOVE = 0x0002;
        private const int SWP_NOSIZE = 0x0001;

        public static void Embedding(IntPtr hWndChild, IntPtr hWndNewParent, int parentWidth,int parentHeight)
        {

            SetParent(hWndChild, hWndNewParent);
            MoveWindow(hWndChild, 0, 0, parentWidth, parentHeight, true);

        }

        public static void Embedding(string pathexe, string args, Control parent)
        {
            string pathfirmapdf = pathexe;
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = pathfirmapdf;
            startInfo.Arguments = args;
            Process p = Process.Start(startInfo);
            IntPtr handler = IntPtr.Zero;
            while (!p.HasExited)
            {
                p.Refresh();
                if (p.MainWindowHandle.ToInt32() != 0)
                {
                    handler = p.MainWindowHandle;
                   
                    break;
                }
            }

            Embedding(handler, parent.Handle, parent.Width, parent.Height);

        }

        public static void SetWindowPos(IntPtr active)
        {
            SetWindowPos((IntPtr)active, HWND_NOTOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);
        }

        public static IDictionary<IntPtr, string> GetOpenedWindowsOrder()
        {
            var openers = GetOpenedWindows();
            IntPtr[] hWnds = openers.Select(x => x.Key).ToArray();
            var z = new int[hWnds.Length];
            for (var i = 0; i < hWnds.Length; i++) z[i] = -1;

            var index = 0;
            var numRemaining = hWnds.Length;
            EnumWindows((wnd, param) =>
            {
                var searchIndex = Array.IndexOf(hWnds, wnd);
                if (searchIndex != -1)
                {
                    z[searchIndex] = wnd.ToInt32();
                    numRemaining--;
                    if (numRemaining == 0) return false;
                }
                index++;
                return true;
            }, 0);


            Dictionary<IntPtr, string> order = new Dictionary<IntPtr, string>();
            for (int p = 0; p < hWnds.Length; p++)
            {
                order.Add(hWnds[p], openers.Where(x => x.Key == (IntPtr)hWnds[p]).First().Value);
            }

            return order;
        }

        public static IDictionary<IntPtr, string> GetOpenedWindows()
        {
            IntPtr shellWindow = GetShellWindow();
            Dictionary<IntPtr, string> windows = new Dictionary<IntPtr, string>();

            EnumWindows(new EnumWindowsProc(delegate (IntPtr hWnd, int lParam) {
                if (hWnd == shellWindow) return true;
                if (!IsWindowVisible(hWnd)) return true;
                int length = GetWindowTextLength(hWnd);
                if (length == 0) return true;
                StringBuilder builder = new StringBuilder(length);
                GetWindowText(hWnd, builder, length + 1);
                //var info = new InfoWindow();
                //info.Handle = hWnd;
                //info.File = new FileInfo(GetProcessPath(hWnd));
                //info.Title = builder.ToString();
                windows[hWnd] = builder.ToString();
                return true;
            }), 0);
            return windows;
        }

        /// <summary>
        /// Returns the caption of all desktop windows.
        /// </summary>
        public static Dictionary<IntPtr, string> GetDesktopWindows()
        {
            mTitlesList = new Dictionary<IntPtr, string>();
            EnumDelegate enumfunc = new EnumDelegate(EnumWindowsProc2);
            IntPtr hDesktop = IntPtr.Zero; // current desktop
            bool success = _EnumDesktopWindows(hDesktop, enumfunc, IntPtr.Zero);

          
            //mTitlesList = mTitlesList.Where(x => x.Value.Trim() != "")
            //                    .ToDictionary(i => i.Key, i => i.Value);

            if (success)
            {
                // Copy the result to string array
                //string[] titles = new string[mTitlesList.Count];
                //mTitlesList.CopyTo(titles);
                return mTitlesList;
            }
            else
            {
                // Get the last Win32 error code
                int errorCode = Marshal.GetLastWin32Error();

                string errorMessage = String.Format(
                "EnumDesktopWindows failed with code {0}.", errorCode);
                throw new Exception(errorMessage);
            }
        }


       





    }
}
