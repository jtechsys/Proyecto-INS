using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doMain.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public class Base64Utils
    {

        /// <summary>
        /// To the base64.
        /// </summary>
        /// <param name="toEnconde">To enconde.</param>
        /// <returns></returns>
        public static string ToBase64(string toEnconde)
        {


            return ToBase64(toEnconde, Encoding.UTF8);
        }

        public static string ToBase64(string text, Encoding encoding)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }

            byte[] textAsBytes = encoding.GetBytes(text);
            return Convert.ToBase64String(textAsBytes);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="base64">The base64.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public static string ToString(string base64)
        {
            

            byte[] data = Convert.FromBase64String(base64);
            return Encoding.UTF8.GetString(data);
        }



       
    }
}
