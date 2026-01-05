using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace SynesthesiaUtil.Extensions;

public static class EnumExtensions
{
    public static string GetDisplayNameAttribute(this Enum enumValue)
    {
        return enumValue.GetType()
            .GetMember(enumValue.ToString())
            .First()
            .GetCustomAttribute<DisplayAttribute>()
            ?.Name ?? enumValue.ToString();
    }

    public static T Random<T>() where T : Enum
    {
        var values = GetEntries<T>();
        var random = new Random();
        var randomIndex = random.Next(values.Count);
        return values[randomIndex];
    }

    public static T Next<T>(this T source) where T : Enum
    {
        var values = GetEntries<T>();
        var nextOrdinal = (source.Ordinal() + 1) % values.Count;
        return values[nextOrdinal];
    }

    public static T Previous<T>(this T source) where T : Enum
    {
        var values = GetEntries<T>();
        var count = values.Count;
        var previousOrdinal = (source.Ordinal() - 1 + count) % count;
        return values[previousOrdinal];
    }

    public static List<T> GetEntries<T>() where T : Enum
    {
        return Enum.GetValues(typeof(T))
            .Cast<T>()
            .ToList();
    }

    public static int Ordinal<T>(this T source) where T : Enum
    {
        return Enum.GetValues(typeof(T)).Cast<T>().ToList().IndexOf(source);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe bool HasFlagFast<T>(this T enumValue, T flag) where T : unmanaged, Enum
    {
        // Note: Using a switch statement would eliminate inlining.

        if (sizeof(T) == 1)
        {
            byte value1 = Unsafe.As<T, byte>(ref enumValue);
            byte value2 = Unsafe.As<T, byte>(ref flag);
            return (value1 & value2) == value2;
        }

        if (sizeof(T) == 2)
        {
            short value1 = Unsafe.As<T, short>(ref enumValue);
            short value2 = Unsafe.As<T, short>(ref flag);
            return (value1 & value2) == value2;
        }

        if (sizeof(T) == 4)
        {
            int value1 = Unsafe.As<T, int>(ref enumValue);
            int value2 = Unsafe.As<T, int>(ref flag);
            return (value1 & value2) == value2;
        }

        if (sizeof(T) == 8)
        {
            long value1 = Unsafe.As<T, long>(ref enumValue);
            long value2 = Unsafe.As<T, long>(ref flag);
            return (value1 & value2) == value2;
        }

        throw new ArgumentException($"Invalid enum type provided: {typeof(T)}.");
    }
}