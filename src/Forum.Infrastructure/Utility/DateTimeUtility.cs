namespace Forum.Infrastructure.Utility
{
    public static class DateTimeUtility
    {
        public static int GetYear(DateTime dateTime1, DateTime dateTime2)
        {
            var res = new DateTime(dateTime1.Year - dateTime2.Year, dateTime1.Month - dateTime2.Month, dateTime1.Day - dateTime2.Day);
            return res.Year;
        }

        public static int GetAge(this DateTime dateTime2)
        {
            var dateTime1 = DateTime.Now;
            var res = dateTime1 - dateTime2;
            var year = res.TotalDays / 365;
            return Convert.ToInt32(year);
        }
    }
}
