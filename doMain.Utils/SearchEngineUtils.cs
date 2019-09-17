using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace doMain.Utils
{
    public class SearchEngineUtils
    {

        public static string QueryBuilder(string column, string searchstring)
        {
            string[] words = searchstring.Split(' ');
            string query = "";
            int pos = 0;
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i] != "")
                {
                    //if (pos == 0)
                        query += " " + String.Format("{0} like '%{1}%' and", column, words[i]);
                    //else 
                    //    query += " " + String.Format("{0} like '%{1}%' and", column, words[i]);     
                }
                pos++;
            }
            return StringUtils.Left(query,query.Length - 3);
        }

    }
}
