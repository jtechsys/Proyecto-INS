using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doMain.Utils
{
    public class PasswordUtils
    {
        public static string GenerateRandomPassword()
        {
            string strPwdchar = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string strPwd = string.Empty;
            Random rnd = new Random();
            for (int i = 0; i <= 7; i++)
            {
                int randomNumber = rnd.Next(0, strPwdchar.Length - 1);
                strPwd += strPwdchar.Substring(randomNumber, 1);
            }
            return strPwd;
        }
    }
}
