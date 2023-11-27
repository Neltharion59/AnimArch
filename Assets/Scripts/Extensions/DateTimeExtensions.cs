using System;

namespace AnimArch.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsJune(this DateTime date)
        {
            return date.Month == 6;
        }
    }
}