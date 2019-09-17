using System;
using System.Collections.Generic;
using System.Text;

namespace doMain.Utils
{
    public class CharacterUtils
    {
        public static string DobleLineaVertical = "||";
        public static char LineaVertical = '|';
        public static char Coma = ',';
        public static char DobleComilla = '"';
        public static string SaltoLinea = "\r\n";
        public static char Ampersan = '&';
        public static char Porcenjate = '%';
        public static char Sombrero = '^';
        public static string Comillas = "\"";
        public static string White = " ";
        public static string HtmlSpace = "&emsp;";
        public static string Tab = "\t";
        

        public static string TabSpace(int count)
        {
            string result = "";
            for (int i = 0; i < count; i++)
            {
                result += Tab; 
            }

            return result;
        }

        public static string WhiteSpace(int count)
        {
            string result = "";
            for (int i = 0; i < count; i++)
            {
                result += White;
            }

            return result;
        }
    }
}
