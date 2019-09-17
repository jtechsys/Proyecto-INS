using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace doMain.Utils
{
    public class CookieHelper
    {
        public static void Add(string key ,string value)
        {
            HttpCookie empresaCookie = new HttpCookie(key + "Cookie");
            empresaCookie.Value = value;
            empresaCookie.Expires = DateTime.Now.AddYears(10);
            HttpContext.Current.Response.Cookies.Add(empresaCookie);
        }

        public static string GetValue(string key)
        {
            if (HttpContext.Current.Request.Cookies[key + "Cookie"] == null)
                return null;

            return HttpContext.Current.Request.Cookies[key + "Cookie"].Value;
        }
    }
}
