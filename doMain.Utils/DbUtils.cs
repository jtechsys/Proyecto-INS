using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doMain.Utils
{
    public static class DbUtils
    {

         

        public static string BuilConnectionString(string connectionString)
        {
            string[] pars = connectionString.Split('|');

            return "data source="+ pars[0] + ";initial catalog=" + pars[1] + ";user id=" + pars[2] + ";password=" + pars[3] + ";MultipleActiveResultSets=True;App=EntityFramework";
        }

    }
}
