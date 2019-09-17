using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace doMain.Utils
{
    public class VariablesUtils
    {
        private static Regex isGuid = new Regex(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", RegexOptions.Compiled);

        public static bool IsDate(object strDate)
        {
            if (strDate == null)
                return false;




            string anyString = strDate.ToString();

            if (anyString == null)
            {
                return false;
            }



            if (anyString.Length > 0)
            {
                System.DateTime dummyOut = DateTime.Now;
                return DateTime.TryParse(anyString, out dummyOut);
            }
            else
            {
                return false;
            }
            //}
        }

        public static bool IsDecimal(object value)
        {

            if (IsNumeric(value) == false)
                return false;

            decimal res = Convert.ToDecimal(value);

            if (value == null)
                return false;


            string anyString = value.ToString();

            if (anyString == null)
            {
                return false;
            }

            if (anyString.Length > 0)
            {
                System.Decimal dummyOut = 0;
                return Decimal.TryParse(anyString, out dummyOut);
            }
            else
            {
                return false;
            }
        }


        public static bool IsNumeric(object Expression)
        {
            bool isNum;
            double retNum;

            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }


        public static bool IsInt(object Expression)
        {
            string cadena = Expression.ToString();

            if (cadena.Contains(",") || cadena.Contains(".") || cadena.Contains("-") || cadena.Contains("+"))
            {
                return false;
            }

            else
            {

                bool isNum;
                Int64 retNum;

                isNum = Int64.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
                return isNum;

            }

        }





        public static bool IsInteger(object value)
        {
            try
            {
                int res = Convert.ToInt32(value);
                return true;
            }
            catch
            {

                return false;
            }

        }


        public static bool IsGuid(string candidate, out Guid output)
        {

            bool isValid = false;

            output = Guid.Empty;

            if (candidate != null)
            {
                if (isGuid.IsMatch(candidate))
                {

                    output = new Guid(candidate);

                    isValid = true;

                }

            }

            return isValid;

        }

        public static bool IsGuid(string candidate)
        {

            bool isValid = false;

            //output = Guid.Empty;

            if (candidate != null)
            {

                if (isGuid.IsMatch(candidate))
                {

                    //output = new Guid(candidate);
                    isValid = true;

                }

            }

            return isValid;

        }
    }
}
