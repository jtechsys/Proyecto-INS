using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace doMain.Utils
{
    public class DecimalUtils
    {

        public static decimal Format(decimal value, int decimalplaces)
        {
            return Math.Round(value, decimalplaces);
        }


    }
}
