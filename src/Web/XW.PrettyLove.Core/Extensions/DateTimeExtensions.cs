namespace XW.PrettyLove.Core
{
    /// <summary>
    /// DateTime扩展类
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 将时间转成unix时间戳
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int ToTicks(this DateTime dt)
        {
            int intResult = 0;
            DateTime startTime = TimeZoneInfo.ConvertTimeToUtc(new DateTime(1970, 1, 1));
            intResult = (int)(dt - startTime).TotalSeconds;
            return intResult;
        }

        /// <summary>
        /// 将Unix时间戳转成时间
        /// </summary>
        /// <param name="ticks"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this int ticks)
        {
            DateTime dtStart = TimeZoneInfo.ConvertTimeToUtc(new DateTime(1970, 1, 1));
            long lTime = long.Parse(ticks + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }

        /// <summary>
        /// 日期转换为时间戳（时间戳单位毫秒）
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long ConvertToTimeStamp(this DateTime time)
        {
            DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)(time.AddHours(-8) - Jan1st1970).TotalMilliseconds;
        }

        /// <summary>
        /// 时间戳转换为日期（时间戳单位秒）
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime ConvertToDateTime(this long timeStamp)
        {
            var start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return start.AddMilliseconds(timeStamp).AddHours(8);
        }

        /// <summary>
        /// 时间戳转换为日期（时间戳单位秒）
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime ConvertToDateTime(this int timeStamp)
        {
            var start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return start.AddMilliseconds(timeStamp).AddHours(8);
        }

        /// <summary>
        /// 返回日期
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static string FormatDate(this DateTime datetime)
        {
            return datetime.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 返回完整日期
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static string FormatString(this DateTime datetime)
        {
            return datetime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 返回完整日期
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string FormtDateString(this DateTime dateTime)
        {
            return $"{dateTime.Year}/{dateTime.Month.ToString().PadLeft(2, '0')}/{dateTime.Day.ToString().PadLeft(2, '0')}";
        }

        /// <summary>
        /// 返回两个时间相差的秒数
        /// </summary>
        /// <param name="dateTime1"></param>
        /// <param name="dateTime2"></param>
        /// <returns></returns>
        public static int GetTotalSeconds(this DateTime dateTime1, DateTime dateTime2)
        {
            var result = dateTime1.Subtract(dateTime2).TotalSeconds;
            return (int)result;
        }

        /// <summary>
        /// 返回指定日期的本月第一天
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static DateTime GetMonthFirstDay(this DateTime datetime)
        {
            return new DateTime(datetime.Year, datetime.Month, 1);
        }

        /// <summary>
        /// 返回指定日期的本月最后一天
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static DateTime GetMonthLastDay(this DateTime datetime)
        {
            return GetMonthFirstDay(datetime).AddMonths(1).AddDays(-1);
        }

        /// <summary>
        /// 得到本周第一天(以星期一为第一天)
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static DateTime GetWeekFirstDay(this DateTime datetime)
        {
            //星期一为第一天
            int weeknow = (int)datetime.DayOfWeek;

            //因为是以星期一为第一天，所以要判断weeknow等于0时，要向前推6天。
            weeknow = (weeknow == 0 ? (7 - 1) : (weeknow - 1));
            int daydiff = (-1) * weeknow;

            //本周第一天
            return datetime.AddDays(daydiff);
        }

        /// <summary>
        /// 得到本周最后一天(以星期天为最后一天)
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static DateTime GetWeekLastDay(this DateTime datetime)
        {
            //星期天为最后一天
            int weeknow = (int)datetime.DayOfWeek;
            weeknow = (weeknow == 0 ? 7 : weeknow);
            int daydiff = (7 - weeknow);
            //本周最后一天
            return datetime.AddDays(daydiff);
        }

        /// <summary>
        /// 计算年龄的方法
        /// </summary>
        /// <param name="birthDate"></param>
        /// <param name="currentDate"></param>
        /// <returns></returns>
        public static int CalculateAge(this DateTime birthDate, DateTime currentDate)
        {
            int age = currentDate.Year - birthDate.Year; // 初步计算年龄

            // 如果当前日期还没到今年的生日，年龄减1
            if (currentDate.Month < birthDate.Month ||
                (currentDate.Month == birthDate.Month && currentDate.Day < birthDate.Day))
            {
                age--;
            }
            return age;
        }
    }
}
