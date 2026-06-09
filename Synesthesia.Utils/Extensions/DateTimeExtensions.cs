// Copyright (c) 2026 SynesthesiaDev <synesthesiadev@proton.me>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace Synesthesia.Utils.Extensions;

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
        if (day is >= 11 and <= 13) return "th";
        return (day % 10) switch
        {
            1 => "st",
            2 => "nd",
            3 => "rd",
            _ => "th"
        };
    }
}
