using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace doMain.Utils
{
    public class UpdaterUtils
    {
        public UpdaterUtils()
        {
        }

        public static void InitializeExe(string name)
        {
            Process.Start(name);
        }
    }
}
