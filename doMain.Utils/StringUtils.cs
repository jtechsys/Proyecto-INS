using System;
using System.Text.RegularExpressions;
using System.Data;
using System.Collections.Generic;
using System.Linq;

namespace doMain.Utils
{
    public static class StringUtils
    {
        public static bool LevelCoincidence(string source, string target, double level)
        {
            int stepsToSame = ComputeLevenshteinDistance(source, target);
            double coincidence = (1.0 - ((double)stepsToSame / (double)Math.Max(source.Length, target.Length)));
            if (coincidence >= level)
            {
                //existe coicidence..
                return true;
            }

            return false;
        }



        static readonly Regex regexDisplay1 = new Regex("[^0-9a-zA-Z]", RegexOptions.Compiled);
        static readonly Regex regexDisplay2 = new Regex("([^A-Z]|^)(([A-Z\\s]*)($|[A-Z]))", RegexOptions.Compiled);
        static readonly Regex regexDisplay3 = new Regex("\\s{2,}", RegexOptions.Compiled);

        public static string FormatClassName(string name)
        {
            string display = name;
            display = regexDisplay1.Replace(display, " ");
            display = regexDisplay2.Replace(display, "$1 $3 $4");
            display = display.Trim();
            display = regexDisplay3.Replace(display, " ");

            var words = display.Split(' ');
            string newword = "";
            for (int w = 0; w < words.Length; w++)
            {
                newword += FirstUpper(words[w].ToLower());
                 
            }

            return newword;
        }

        public static string FormatCaptionDisplay(string str)
        {
            string display = str;
            display = regexDisplay1.Replace(display, " ");
            display = regexDisplay2.Replace(display, "$1 $3 $4");
            display = display.Trim();
            display = regexDisplay3.Replace(display, " ");

            var words = display.Split(' ');
            string newword = "";
            for (int w = 0; w < words.Length; w++)
            {
                newword += FirstUpper(words[w].ToLower()) + " ";

            }

            return newword.Trim();
        }

        public static List<string> FindWords(string str, string contain)
        {
            var words = new List<string>(str.Split(' '));
            return words.Where(x => x.Contains(contain)).ToList();
        }

        public static int FindPosition(string str, string word)
        {
            //String testing = "text that i am looking for";
            //Console.Write(testing.IndexOf("looking") + Environment.NewLine);
            return str.IndexOf(word);

            

        }

        public static int ComputeLevenshteinDistance(string source, string target)
        {
            if ((source == null) || (target == null)) return 0;
            if ((source.Length == 0) || (target.Length == 0)) return 0;
            if (source == target) return source.Length;

            int sourceWordCount = source.Length;
            int targetWordCount = target.Length;

            // Step 1
            if (sourceWordCount == 0)
                return targetWordCount;

            if (targetWordCount == 0)
                return sourceWordCount;

            int[,] distance = new int[sourceWordCount + 1, targetWordCount + 1];

            // Step 2
            for (int i = 0; i <= sourceWordCount; distance[i, 0] = i++) ;
            for (int j = 0; j <= targetWordCount; distance[0, j] = j++) ;

            for (int i = 1; i <= sourceWordCount; i++)
            {
                for (int j = 1; j <= targetWordCount; j++)
                {
                    // Step 3
                    int cost = (target[j - 1] == source[i - 1]) ? 0 : 1;

                    // Step 4
                    distance[i, j] = Math.Min(Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1), distance[i - 1, j - 1] + cost);
                }
            }

            return distance[sourceWordCount, targetWordCount];
        }


        public static string ArrayToString(string[] array, char splitvar)
        {
            string trama = "";
            for (int i = 0; i < array.Length; i++)
            {
                trama += array[i] + splitvar.ToString();
            }
            trama = Left(trama, trama.Length - 1);
            return trama;
        }

        public static string[] StringToArray(string trama, Char splitvar)
        {

            if (trama == null)
                return null;

            string[] varpair = trama.Split(splitvar);

            string[] array = new string[varpair.Length];
            for (int i = 0; i < varpair.Length; i++)
            {
                array.SetValue(varpair[i], i);
            }

            return array;
        }

        public static string[] Split(string str, string pattern)
        {
            if (str == null)
                return null;

            string[] lines = Regex.Split(str, pattern);



            return lines;

        }

        public static string NumericRandonFormat()
        {
            Random randomNumber = new Random();
            int rndNum = randomNumber.Next(1, 9999999);
            return Right("0000000" + rndNum.ToString(), 7);
        }

        public static string SerieRandonFormat()
        {
            Random randomNumber = new Random();
            int rndNum = randomNumber.Next(1, 999);
            return Right("000" + rndNum.ToString(), 3);
        }

        public static string RowToTrama(DataTable dt, string Column)
        {
            string trama = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                trama += dt.Rows[i][Column].ToString() + ",";
            }
            if (trama.Length > 1)
                trama = trama.Substring(0, trama.Length - 1);

            return trama;
        }

        public static string RemoveLeft(string param, int length)
        {
            return Right(param, param.Length - length);
        }

        public static string Left(string param, int length)
        {
            if (param == "")
                return param;

            if (param.Length < length)
            {
                return param;
            }

            //we start at 0 since we want to get the characters starting from the
            //left and with the specified lenght and assign it to a variable
            string result = param.Substring(0, length);
            //return the result of the operation
             return result; 
        }
        public static string Right(string param, int length)
        {

            if (param == "")
                return "";

            //start at the index based on the lenght of the sting minus
            //the specified lenght and assign it a variable
            string result = param.Substring(param.Length - length, length);
            //return the result of the operation
             return result; 
        }
        private static string SQLDateTime(DateTime fecha)
        {
            string year = fecha.Year.ToString();
            string month = StringUtils.Right("00" + fecha.Month.ToString(), 2);
            string day = StringUtils.Right("00" + fecha.Day.ToString(), 2);

            return year + month + day;
        }

        public static string SQLDateTime(DateTime fechaInicio, DateTime fechaFin)
        {
            string inicio = "";
            inicio = SQLDateTime(fechaInicio);

            TimeSpan ts = fechaFin - fechaInicio;
            int days = ts.Days + 1;

            string sql = " Between '" + inicio + "' and datediff(d,-" + days.ToString() + ",'" + inicio + "') ";
            return sql;
        }

        public static string Replace(string word,string pattern,string replace, int count)
        {
            var regex = new Regex(Regex.Escape(pattern));
            return regex.Replace(word, replace, count);
        }


        public static string FirstUpper(string word)
        {
            if (word == null)
                return "";

            if (word == "")
                return "";

            //word = word.ToLower();

            string fstLetter = word.Substring(0, 1);
            return fstLetter.ToUpper() + word.Remove(0, 1);
        }

        public static string FirstLower(string word)
        {
            if (word == null)
                return "";

            if (word == "")
                return "";

            //word = word.ToLower();

            string fstLetter = word.Substring(0, 1);
            return fstLetter.ToLower() + word.Remove(0, 1);
        }

        public static int DaysDateTime(DateTime fechaInicio, DateTime fechaFin)
        {
            //string inicio = "";
            //inicio = SQLDateTime(fechaInicio);

            TimeSpan ts = fechaFin - fechaInicio;
            int days = ts.Days + 1;
            return days;
        }

        private static Regex isGuid = new Regex(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", RegexOptions.Compiled);

        public static bool IsGuid(string candidate)
        {
            if (candidate != null)
            {
                if (isGuid.IsMatch(candidate))
                {
                    return true;
                }
            }

            return false;
        }

        //public static string FirstLetterToUpper(string str)
        //{
        //    if (str == null)
        //        return null;

        //    if (str.Length > 1)
        //        return char.ToUpper(str[0]) + str.Substring(1);

        //    return str.ToUpper();
        //}
    }
}
