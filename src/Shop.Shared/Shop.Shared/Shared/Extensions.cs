using System;

namespace Shop.Shared.Shared
{
    public static class Extensions
    {
        public static bool IsEmpty(this string value) => string.IsNullOrEmpty(value);
        public static long ToUnixEpochDate(this DateTime date)
            => (long)Math.Round((date.ToUniversalTime() -
                                 new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                .TotalSeconds);
    }
}
