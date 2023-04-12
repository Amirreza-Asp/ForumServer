using System.Globalization;

namespace Forum.Application.Utility
{
    public static class DateTimeUtility
    {

        public static String ToISO(this DateTime date)
        {
            var utcDate = new DateTime(date.Year, date.Month, date.Day,
                date.Hour, date.Minute, date.Second, DateTimeKind.Utc);

            return utcDate.ToString("o");
        }

        public static String ToMonthName(int monthNumber)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(monthNumber);
        }

    }
}
