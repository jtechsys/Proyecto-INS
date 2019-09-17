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
    public class HtmlUtils
    {

        /// <summary>
        /// Strings to encode.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <returns></returns>
        public static string StringToEncode(string html)
        {
            html = html.Replace("á", "&aacute").Replace("é", "&eacute").Replace("í", "&iacute").Replace("ó", "&oacute").Replace("ú", "&uacute").Replace("ñ", "&ntilde").Replace("Á", "&Aacute").Replace("É", "&Eacute").Replace("Ó", "&Oacute").Replace("Ú", "&Uacute").Replace("Ñ", "&Ntilde");
            return html;
        }

    }
}
