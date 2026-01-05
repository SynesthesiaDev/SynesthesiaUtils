using System;

namespace SynesthesiaUtil.Extensions;

public static class DateTimeExtensions
{
    public static DateTimeOffset ToDateTime(this long time)
    {
        return DateTimeOffset.FromUnixTimeMilliseconds(time);
    }


    public static string ToProperlyFormatted(this DateTimeOffset dateTimeOffset)
    {
        return GetProperFormattedDate(dateTimeOffset);
    }

    public static string GetProperFormattedDate(DateTimeOffset dateTimeOffset)
    {
        var day = dateTimeOffset.Day;

        return $"{day}{GetDaySuffix(day)} of {dateTimeOffset:MMMM yyyy}";
    }

    public static string GetDaySuffix(int day)
    {
        if (day >= 11 && day <= 13) return "th";
        return (day % 10) switch
        {
            1 => "st",
            2 => "nd",
            3 => "rd",
            _ => "th"
        };
    }
}