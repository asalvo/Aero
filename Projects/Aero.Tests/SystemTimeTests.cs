using System;
using Xunit;

namespace Aero.Common
{
    public class SystemTimeTests
    {
        public SystemTimeTests()
        {
            SystemTime.Reset();
        }

        ~SystemTimeTests()
        {
            SystemTime.Reset();
        }

        [Fact]
        public void Default_Value_Of_SystemTime_DateTimeOffsetNow_Should_Be_LocalTime()
        {
            Assert.Equal(DateTimeOffset.Now.Date, SystemTime.DateTimeOffsetNow().Date);
            Assert.Equal(DateTimeOffset.Now.Hour, SystemTime.DateTimeOffsetNow().Hour);
        }

        [Fact]
        public void Set_Time_Test()
        {
            SystemTime.DateTimeOffsetNow = () => DateTimeOffset.Parse("1/1/2011 13:45");

            Assert.Equal(DateTimeOffset.Parse("1/1/2011 13:45"), SystemTime.DateTimeOffsetNow());
        }
    }
}
