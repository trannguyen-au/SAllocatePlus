using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Tna.SAllocatePlus.CommonShared
{
    public class Tools
    {
        public static DateTime ParseDateTimeAUFormat(string datetimeString)
        {
            System.IFormatProvider format =
                new System.Globalization.CultureInfo("en-AU", true);
            return DateTime.Parse(datetimeString, format);
        }

        public static DateTime? TryParseDateTimeAuFormat(string dateTimeString)
        {
            System.IFormatProvider format =
                new System.Globalization.CultureInfo("en-AU", true);
            DateTime value = DateTime.MinValue;
            if (DateTime.TryParse(dateTimeString, format, System.Globalization.DateTimeStyles.None, out value))
            {
                return value;
            }
            return null;
        }

        public static DateTime FirstDayOfMonth(DateTime now)
        {
            return new DateTime(now.Year, now.Month, 1, 0, 0, 0);
        }

        public static DateTime LastDayOfMonth(DateTime now)
        {
            var nextMonth = now.AddMonths(1);
            var lastDayOfMonth = new DateTime(nextMonth.Year, nextMonth.Month, 1, 0, 0, 0).AddSeconds(-1);
            return lastDayOfMonth;
        }

        public static DateTime NextMonday()
        {
            DateTime now = DateTime.Today;

            DateTime nextMonday = now;

            switch (now.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    nextMonday = now.AddDays(7);
                    break;
                case DayOfWeek.Tuesday:
                    nextMonday = now.AddDays(6);
                    break;
                case DayOfWeek.Wednesday:
                    nextMonday = now.AddDays(5);
                    break;
                case DayOfWeek.Thursday:
                    nextMonday = now.AddDays(4);
                    break;
                case DayOfWeek.Friday:
                    nextMonday = now.AddDays(3);
                    break;
                case DayOfWeek.Saturday:
                    nextMonday = now.AddDays(2);
                    break;
                case DayOfWeek.Sunday:
                    nextMonday = now.AddDays(1);
                    break;
                default:
                    break;

            }
            return nextMonday;
        }

        public static string TrimText(string value)
        {
            if (string.IsNullOrEmpty(value)) return value;
            else return value.Trim();
        }

        public static void TrimStringMember<T>(T entity) where T : class
        {
            if (entity == null) return;
            var pinfo = entity.GetType().GetProperties();
            foreach (var pi in pinfo)
            {
                if (pi.CanRead && pi.CanWrite && pi.PropertyType == typeof(string))
                {
                    pi.SetValue(entity, TrimText(pi.GetValue(entity, null)), null);
                }
            }
        }

        public static string TrimText(object value)
        {
            if (value == DBNull.Value || value == null) return null;
            return value.ToString().Trim();
        }

        public static string FormatDateDisplay(DateTime dateTime, string format="yyyy/MM/dd")
        {
            return dateTime.ToString(format);
        }

        public static bool isDbNullOrEmpty(object cellValue)
        {
            return cellValue == null || cellValue == DBNull.Value;
        }

        public static bool isValidTimeValue(string timeValue)
        {
            if (string.IsNullOrWhiteSpace(timeValue)) return false;
            if (timeValue.Contains(":"))
            {
                var timeComponents = timeValue.Split(":".ToCharArray());
                try
                {
                    int hour = int.Parse(timeComponents[0]);
                    int minute = int.Parse(timeComponents[1]);
                    if (hour < 24 && minute < 60 && hour >= 0 && minute >= 0)
                    {
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }

        public static void MoveFileOrReplace(string tempFilePath, string targetFilePath)
        {
            if (File.Exists(targetFilePath))
            {
                File.Delete(targetFilePath);
            }
            File.Move(tempFilePath, targetFilePath);
        }

        public static void DeleteFileIfExist(string file)
        {
            if (File.Exists(file))
            {
                File.Delete(file);
            }
        }

        public static string FormatTimeDisplay(TimeSpan? ts)
        {
            if (ts == null) return "00:00";
            else return ts.Value.Hours.ToString("00") + ":" + ts.Value.Minutes.ToString("00");
        }

        public static DateTime? ConvertTimeSpanToDateTime(TimeSpan? ts)
        {
            if (ts == null) return null;
            else return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, ts.Value.Hours, ts.Value.Minutes, ts.Value.Seconds);
        }

        public static bool IsNullOrEmpty(ICollection list)
        {
            return list == null || list.Count == 0;
        }
    }
}
