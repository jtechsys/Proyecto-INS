using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;


namespace doMain.Utils
{

    public class DateTimeUtils
    {


        public static bool IsDate(string date)
        {

            try

            {

                DateTime dt = DateTime.Parse(date);

                return true;
            }

            catch

            {

                return false;

            }

        }

        public static List<string> GetTimeIntervals()
        {
            List<string> timeIntervals = new List<string>();
            TimeSpan startTime = new TimeSpan(0, 0, 0);
            DateTime startDate = new DateTime(DateTime.MinValue.Ticks); // Date to be used to get shortTime format.
            for (int i = 0; i < 48; i++)
            {
                int minutesToBeAdded = 30 * i;      // Increasing minutes by 30 minutes interval
                TimeSpan timeToBeAdded = new TimeSpan(0, minutesToBeAdded, 0);
                TimeSpan t = startTime.Add(timeToBeAdded);
                DateTime result = startDate + t;
                timeIntervals.Add(result.ToShortTimeString());      // Use Date.ToShortTimeString() method to get the desired format                
            }
            return timeIntervals;
        }
        public static DateTime ChangeTime(DateTime dateTime, int hours, int minutes, int seconds, int milliseconds)
        {
            return new DateTime(
                dateTime.Year,
                dateTime.Month,
                dateTime.Day,
                hours,
                minutes,
                seconds,
                milliseconds,
                dateTime.Kind);
        }

        //public static String GetMonthName(int month)
        //{
        //    System.Globalization.DateTimeFormatInfo mfi = new
        //    System.Globalization.DateTimeFormatInfo();
        //    return mfi.GetMonthName(month).ToString(); w


        //}

        public static String GetMonthName(int month)
        {
            DateTimeFormatInfo dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;
            return  StringUtils.FirstUpper(dtinfo.GetMonthName(month));
           
         
        }

        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyy-MM-ddTHH:mm:ss.ffff");
        }

        /// <summary>
        /// Formato ht.add(id,new TimeSpan[]{horainicio,horafin};
        /// </summary>
        /// <param name="hours"></param>
        /// <returns>Si devuelve "" es que fue correcto</returns>
        public static string ValidarRangoHoras(object id, int day , TimeSpan start, TimeSpan end, Hashtable hours)
        {
            if (start >= end)
                return "La hora de inicio tiene que ser menor a la hora final";

            IDictionaryEnumerator de = hours.GetEnumerator();
            while (de.MoveNext())
            {

                if (day != Convert.ToInt32(((object[])de.Value)[0]))
                    continue;
                if (id.ToString() == de.Key.ToString())
                    continue;


                //validar rango
                TimeSpan tsStart = (TimeSpan)((object[])de.Value)[1];
                TimeSpan tsEnd = (TimeSpan)((object[])de.Value)[2];

                if (tsStart <= start && start <= tsEnd)
                    return "Hora inicio no es valido.. esta dentro del rango " + tsStart.ToString() + " - " + tsEnd.ToString();

                if (tsStart <= end && end <= tsEnd)
                    return "Hora final no es valido.. esta dentro del rango " + tsStart.ToString() + " - "  + tsEnd.ToString();

                if (start <= tsStart && tsStart <= end)
                    return "Rango no valido.. se encuentra dentro del " + tsStart.ToString() + " - " + tsEnd.ToString();

                if (start <= tsEnd && tsEnd <= end)
                    return "Rango no valido.. se encuentra dentro del " + tsStart.ToString() + " - " + tsEnd.ToString();
            }

            return "";
        }


        public static int CantidadDias(int MesInicio, int MesFin, int AñoEnCurso)
        {
            int days = 0;
            for (int i = MesInicio; i <= MesFin; i++)
            {
                days += DateTime.DaysInMonth(AñoEnCurso, i);
            }

            return days;
        }


        /// <summary>
        /// coloca el relojero en el inicio del dia
        /// </summary>
        /// <returns></returns>
        public static DateTime InitDay(DateTime date)
        {            
            return new DateTime(date.Year, date.Month, date.Day);
        }


        public static DateTime EndDay(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
        }

        public static DataTable DiasSemanaTabla()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Day");
            dt.Columns.Add("Value");

            dt.Rows.Add("Lunes", 2);
            dt.Rows.Add("Martes", 3);
            dt.Rows.Add("Miercoles", 4);
            dt.Rows.Add("Jueves", 5);
            dt.Rows.Add("Viernes", 6);
            dt.Rows.Add("Sabado", 7);
            dt.Rows.Add("Domingo", 1);

            return dt;
        }

        public static StringCollection DiasSemana()
        {
            StringCollection dias = new StringCollection();
            dias.Add("Domingo");
            dias.Add("Lunes");
            dias.Add("Martes");
            dias.Add("Miercoles");
            dias.Add("Jueves");
            dias.Add("Viernes");
            dias.Add("Sabado");

            return dias;
        }

        public static string DiasSemana(int dia)
        {
            string ndia = "";
            switch (dia)
            {
                case 2:
                    ndia = "Lunes";
                    break;
                case 3:
                    ndia = "Martes";
                    break;
                case 4:
                    ndia = "Miercoles";
                    break;
                case 5:
                    ndia = "Jueves";
                    break;
                case 6:
                    ndia = "Viernes";
                    break;
                case 7:
                    ndia = "Sabado";
                    break;
                case 1:
                    ndia = "Domingo";
                    break;
            }

            return ndia;
        }


        public static int NumeroDiaSemana(DateTime fecha)
        {
            int dia = 0;
            DayOfWeek diasemana = fecha.DayOfWeek;
            switch (diasemana)
            {

                case DayOfWeek.Sunday:
                    dia = 1;
                    break;
                case DayOfWeek.Monday:
                    dia = 2;
                    break;
                case DayOfWeek.Tuesday:
                    dia = 3;
                    break;
                case DayOfWeek.Wednesday:
                    dia = 4;
                    break;
                case DayOfWeek.Thursday:
                    dia = 5;
                    break;
                case DayOfWeek.Friday:
                    dia = 6;
                    break;
                case DayOfWeek.Saturday:
                    dia = 7;
                    break;
            }

            return dia;
        }

        public static int DateTimeToInt(DateTime theDate)
        {
            int unixTime = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
            return unixTime;
        }

        public static string SQLRangoFechas3(DateTime FechaInicio, DateTime FechaFinal)
        {
            string inicio = "";
            inicio = FormatearFecha3(FechaInicio);
            string fin = FormatearFecha3(FechaFinal);

            //TimeSpan ts = FechaFinal - FechaInicio;
            //int days = ts.Days + 1;

            string sql = " Between '" + inicio + " 00:00:00' and '" + fin + " 23:59:59' ";

            //string inicio = DateTimeToInt(FechaInicio).ToString();
            //string fin = DateTimeToInt(FechaFinal).ToString();

            //string sql = " Between " + inicio + "  and " + fin + "  ";


            return sql;
        }

        public static string SQLRangoFechas(DateTime FechaInicio, DateTime FechaFinal)
        {
            string inicio = "";
            inicio = FormatearFecha(FechaInicio);

            TimeSpan ts = FechaFinal - FechaInicio;
            int days = ts.Days + 1;

            string sql = " Between '" + inicio + "' and datediff(d,-" + days.ToString() + ",'" + inicio + "') ";


            return sql;
        }

        public static string SQLRangoFechas2(DateTime FechaInicio, DateTime FechaFinal)
        {
            string inicio = "";
            inicio = FormatearFecha2(FechaInicio);
            string fin = "";
            fin = FormatearFecha2(FechaFinal);


            string sql = " Between '" + inicio + "' and  '" + fin + "'";
            return sql;
        }

        public static string SQLRangoFechasHora2(DateTime FechaInicio, DateTime FechaFinal)
        {
            string inicio = "";
            inicio = FormatearFechaHora2(FechaInicio);
            string fin = "";
            fin = FormatearFechaHora2(FechaFinal);


            string sql = " Between '" + inicio + "' and  '" + fin + "'";
            return sql;
        }

        public static string SQLRangoFechas3(string columnName, DateTime FechaInicio, DateTime FechaFinal)
        {
            string inicio = "";
            inicio = formatearFecha3(FechaInicio);
            string fin = "";
            fin = formatearFecha3(FechaFinal.AddDays(1));


            string sql = columnName + " >= '" + inicio + "' and " + columnName + "  < '" + fin + "'";

            return sql;
        }


        public static string FormatearFecha(DateTime fecha)
        {
            string year = fecha.Year.ToString();
            string month = StringUtils.Right("00" + fecha.Month.ToString(), 2);
            string day = StringUtils.Right("00" + fecha.Day.ToString(), 2);

            return year + month + day;
        }

        /// <summary>
        /// Retorna un formato de fecha : año-mes-dia
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public static string FormatearFecha2(DateTime fecha)
        {
            string year = fecha.Year.ToString();
            string month = StringUtils.Right("00" + fecha.Month.ToString(), 2);
            string day = StringUtils.Right("00" + fecha.Day.ToString(), 2);

            return year + "-" + month + "-" + day;
        }

        //public static string c(DateTime fecha)
        //{
        //    string year = fecha.Year.ToString();
        //    string month = StringUtils.Right("00" + fecha.Month.ToString(), 2);
        //    string day = StringUtils.Right("00" + fecha.Day.ToString(), 2);

        //    return year + "/" + month + "/" + day;
        //}


        //////public static string FormatearFechaHora(DateTime fecha)
        //////{
        //////    //string year = fecha.Year.ToString();
        //////    //string month = StringUtils.Right("00" + fecha.Month.ToString(), 2);
        //////    //string day = StringUtils.Right("00" + fecha.Day.ToString(), 2);

        //////    //return day +"/"+ month + "/" + year + " " + fecha.Hour.ToString() + ":" + fecha.Minute.ToString() + ":"+ fecha.Second.ToString();
        //////    return fecha.ToString("yyyy-M-d HH:mm:ss}");
        //////}


        public static string FormatearFechaHora(DateTime fecha)
        {
            string year = fecha.Year.ToString();
            string month = StringUtils.Right("00" + fecha.Month.ToString(), 2);
            string day = StringUtils.Right("00" + fecha.Day.ToString(), 2);

            return day + "/" + month + "/" + year + " " + fecha.Hour.ToString() + ":" + fecha.Minute.ToString() + ":" + fecha.Second.ToString();

        }


        public static string FormatearFechaHoraEnglish(DateTime fecha)
        {
            string year = fecha.Year.ToString();
            string month = StringUtils.Right("00" + fecha.Month.ToString(), 2);
            string day = StringUtils.Right("00" + fecha.Day.ToString(), 2);

            return month + "/" + day + "/" + year + " " + fecha.Hour.ToString() + ":" + fecha.Minute.ToString() + ":" + fecha.Second.ToString();

        }


        public static string FormatearFecha3(DateTime fecha)
        {
            string year = fecha.Year.ToString();
            string month = StringUtils.Right("00" + fecha.Month.ToString(), 2);
            string day = StringUtils.Right("00" + fecha.Day.ToString(), 2);

            return day + "/" + month + "/" + year;

        }

        public static string FormatearFecha4(DateTime fecha)
        {
            string year = fecha.Year.ToString();
            string month = StringUtils.Right("00" + fecha.Month.ToString(), 2);
            string day = StringUtils.Right("00" + fecha.Day.ToString(), 2);

            return year + "/" + month + "/" + day;

        }

        public static string FormatearFecha5(DateTime fecha)
        {
            //string year = fecha.Year.ToString();
            //string month = StringUtils.Right("00" + fecha.Month.ToString(), 2);
            //string day = StringUtils.Right("00" + fecha.Day.ToString(), 2);

            //return day + "/" + month + "/" + year;
            return " CONVERT(VARCHAR,'" + fecha.ToShortDateString() + "' , 103) ";
        }


       
        public static string FormatearFechaHora2(DateTime fecha)
        {
            string year = fecha.Year.ToString();
            string month = StringUtils.Right("00" + fecha.Month.ToString(), 2);
            string day = StringUtils.Right("00" + fecha.Day.ToString(), 2);

            return year + "-" + month + "-" + day + " " + fecha.Hour.ToString() + ":" + fecha.Minute.ToString() + ":" + fecha.Second.ToString();

        }

        public static DateTime ConvertirSegundosAMinutos(int segundos)
        {

            int tiempo = segundos;
            int horas = segundos / 3600;
            int resto_horas = segundos % 3600;
            int minutos = resto_horas / 60;
            int resto_minutos = resto_horas % 60;
            int segundo = resto_minutos;

            return  new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,  horas, minutos, segundo);

        }


        


        private static string formatearFecha3(DateTime fecha)
        {
            string year = fecha.Year.ToString();
            string month = StringUtils.Right("00" + fecha.Month.ToString(), 2);
            string day = StringUtils.Right("00" + fecha.Day.ToString(), 2);

            return month + "/" + day + "/" + year;
        }


        public static double HourDiff(TimeSpan init, TimeSpan end)
        {
            return end.Subtract(init).TotalHours;
        }

        public static decimal DateDiff(DateInterval interval, DateTime date1, DateTime date2)
        {

            TimeSpan ts = ts = date2 - date1;
            switch (interval)
            {
                case DateInterval.Year:
                    return date2.Year - date1.Year;
                case DateInterval.Month:
                    return (date2.Month - date1.Month) + (12 * (date2.Year - date1.Year));
                case DateInterval.Weekday:
                    return Fix(ts.TotalDays) / 7;
                case DateInterval.Day:
                    return Fix(ts.TotalDays);
                case DateInterval.Hour:
                    return (decimal)ts.TotalHours;
                case DateInterval.Minute:
                    return Fix(ts.TotalMinutes);
                default:
                    return Fix(ts.TotalSeconds);
            }
        }

        private static decimal Fix(double Number)
        {
            if (Number >= 0)
            {
                return (decimal)Math.Floor(Number);
            }
            return (decimal)Math.Ceiling(Number);
        }

        
        public static DateTime ConvertToDate(int? integerDate)
        {
            return new DateTime(1900, 1, 1).AddDays(integerDate.Value - 2);
        }

        public static int? ConvertToInt(DateTime? theDate)
        {
            if (theDate == null)
                return null;

            return (int)(theDate.Value.Date - new DateTime(1900, 1, 1)).TotalDays + 2;
        }


    }
}
