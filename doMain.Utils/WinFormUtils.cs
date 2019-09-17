using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace doMain.Utils
{
    public class WinFormUtils
    {
        public static bool TopMost
        {
            get;
            set;
        }

        public WinFormUtils()
        {
        }

        public static void Configuration(Form form)
        {
            form.TopMost = WinFormUtils.TopMost;
        }

        public static IEnumerable<Control> GetAllControls(Control container)
        {
            List<Control> controlList = new List<Control>();
            foreach (Control c in container.Controls)
            {
                controlList.AddRange(GetAllControls(c));
                controlList.Add(c);
            }
            return controlList;
        }
    }
}
