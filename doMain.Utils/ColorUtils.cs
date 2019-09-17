using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace doMain.Utils
{
    public class ColorUtils
    {
        public static Color ConvertFromString(string rgb)
        {
            return (Color)new ColorConverter().ConvertFromString(rgb);
        }
    }
}
