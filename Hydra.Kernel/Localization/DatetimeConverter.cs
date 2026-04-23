using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hydra.Kernel.Localization
{
    public static class DatetimeConverter
    {
        public static string GetFullPersianDatetime(string datetime)
        {
            DateTime dt;
            if (DateTime.TryParse(datetime, out dt))
            {
                PersianCalendar pc = new PersianCalendar();
                string Year = pc.GetYear(dt).ToString();
                string Month = pc.GetMonth(dt).ToString("D2");
                string Day = pc.GetDayOfMonth(dt).ToString("D2");
                int CorrectDay = int.Parse(Day);
                switch (Month)
                {
                    case "01":
                        Month = "فروردین";
                        break;
                    case "02":
                        Month = "اردیبهشت";
                        break;
                    case "03":
                        Month = "خرداد";
                        break;
                    case "04":
                        Month = "تیر";
                        break;
                    case "05":
                        Month = "مرداد";
                        break;
                    case "06":
                        Month = "شهریور";
                        break;
                    case "07":
                        Month = "مهر";
                        break;
                    case "08":
                        Month = "آبان";
                        break;
                    case "09":
                        Month = "آذر";
                        break;
                    case "10":
                        Month = "دی";
                        break;
                    case "11":
                        Month = "بهمن";
                        break;
                    case "12":
                        Month = "اسفند";
                        break;
                    default:
                        break;
                }
                string week = dt.DayOfWeek.ToString();
                string Aweek = "";
                switch (week)
                {
                    case "Saturday":
                        Aweek = "شنبه";
                        break;
                    case "Sunday":
                        Aweek = "یکشنبه";
                        break;
                    case "Monday":
                        Aweek = "دوشنبه";
                        break;
                    case "Tuesday":
                        Aweek = "سه شنبه";
                        break;
                    case "Wednesday":
                        Aweek = "چهارشنبه";
                        break;
                    case "Thursday":
                        Aweek = "پنجشنبه";
                        break;
                    case "Friday":
                        Aweek = "جمعه";
                        break;
                }


                return Aweek + " " + CorrectDay + " " + Month + " " + Year;
            }
            return "-";
        }
        /// <summary>
        /// Result example: 1404/05/26 22:10:26
        /// </summary>
        /// <param name="datetime"></param>
        /// <param name="dateSeperator"></param>
        /// <param name="timeSeperator"></param>
        /// <returns></returns>
        public static string GetPersianDatetime(DateTime datetime, string dateSeperator = "/", string timeSeperator = " ")
        {
            PersianCalendar pc = new PersianCalendar();
            string Year = pc.GetYear(datetime).ToString();
            string Month = pc.GetMonth(datetime).ToString("D2");
            string Day = pc.GetDayOfMonth(datetime).ToString("D2");
            string hour = pc.GetHour(datetime).ToString("D2");
            string minute = pc.GetMinute(datetime).ToString("D2");
            string second = pc.GetSecond(datetime).ToString("D2");


            return Year + dateSeperator + Month + dateSeperator + Day + timeSeperator + hour + ":" + minute + ":" + second;
        }
        /// <summary>
        /// Result example: 1404
        /// </summary>
        /// <param name="datetime"></param>
        /// <param name="dateSeperator"></param>
        /// <param name="timeSeperator"></param>
        /// <returns></returns>
        public static int GetPersianYear(DateTime datetime)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(datetime);
        }

    }
}
