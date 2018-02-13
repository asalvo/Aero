using System;

namespace Aero
{
    public static class SystemTime
    {
        public static Func<DateTime> DateTimeUtcNow = () => DateTime.UtcNow;
        public static Func<DateTimeOffset> DateTimeOffsetNow = () => DateTimeOffset.Now;

        public static void Reset()
        {
            DateTimeUtcNow = () => DateTime.UtcNow;
            DateTimeOffsetNow = () => DateTimeOffset.Now;
        }
    }
}
