using System;
using System.Collections.Generic;
using System.Text;

namespace ChallengeNubimetrics.Application.Helpers
{
    public static class DateTimeHelper
    {
        ///https://stackoverflow.com/questions/14149346/what-value-should-i-pass-into-timezoneinfo-findsystemtimezonebyidstring
        private const string TIMEZONE_USAGE = "Argentina Standard Time";

        public static DateTime GetSystemDate()
            => TimeZoneInfo.ConvertTimeFromUtc(
                DateTime.UtcNow,
                TimeZoneInfo.FindSystemTimeZoneById(TIMEZONE_USAGE)
                );

    }
}
