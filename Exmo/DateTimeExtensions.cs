using System;

namespace Exmo
{
    public static class DateTimeExtensions
    {
        private static TimeSpan GetUnixTimeSpan(this DateTime value)
        {
            return value.ToUniversalTime().Subtract(UnixTime.Epoch);
        }

        public static long ToUnixTime(this DateTime value)
        {
            return (long)GetUnixTimeSpan(value).TotalSeconds;
        }

        public static long ToUnixTimeMilliseconds(this DateTime value)
        {
            return (long)GetUnixTimeSpan(value).TotalMilliseconds;
        }

        public static long ToUnixTimeSeconds(this DateTime value)
        {
            return (long)GetUnixTimeSpan(value).TotalSeconds;
        }
    }
}
