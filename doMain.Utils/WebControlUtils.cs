using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace doMain.Utils
{
    public static class WebControlUtils
    {


        public static Control FindControlRecursive(this Control control, string id)
        {
            if (control == null) return null;
            //try to find the control at the current level
            Control ctrl = control.FindControl(id);

            if (ctrl == null)
            {
                //search the children
                foreach (Control child in control.Controls)
                {
                    ctrl = FindControlRecursive(child, id);

                    if (ctrl != null) break;
                }
            }
            return ctrl;
        }

        //public static List<Control> AllControls = new List<Control>();
        //public static void GetAllControlsInWebPage(Control oControl)
        //{
        //    foreach (Control childControl in oControl.Controls)
        //    {

        //        AllControls.Add(childControl); //lstControl - Global variable
        //        if (childControl.HasControls())
        //            GetAllControlsInWebPage(childControl);

        //    }
        //}

    }
}
