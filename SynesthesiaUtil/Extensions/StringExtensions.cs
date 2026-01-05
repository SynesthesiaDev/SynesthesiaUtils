using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SynesthesiaUtil.Extensions;

public static class StringExtensions
{
    public static int ToInt(this string source)
    {
        return int.Parse(source);
    }

    public static double ToDouble(this string source)
    {
        return double.Parse(source);
    }

    public static float ToFloat(this string source)
    {
        return float.Parse(source);
    }

    public static byte ToByte(this string source)
    {
        return byte.Parse(source);
    }

    public static short ToShort(this string source)
    {
        return short.Parse(source);
    }

    public static long ToLong(this string source)
    {
        return long.Parse(source);
    }

    public static bool ToBoolean(this string source)
    {
        return source.ToLower().Equals("true");
    }

    public static string RemovePrefix(this string source, string prefix)
    {
        if (string.IsNullOrEmpty(prefix)) return source;

        return source.StartsWith(prefix) ? source.Substring(prefix.Length) : source;
    }

    public static string RemoveSuffix(this string source, string suffix)
    {
        if (string.IsNullOrEmpty(source)) return source;

        return source.EndsWith(suffix) ? source.Substring(0, source.Length - suffix.Length) : source;
    }

    public static bool IsEmpty(this string source)
    {
        return string.IsNullOrEmpty(source);
    }

    public static Tuple<int, int, int> HexToRgb(this string source)
    {
        var rgb = formatHexString(2, false, source);
        return new Tuple<int, int, int>(rgb[0], rgb[1], rgb[2]);
    }

    public static Tuple<int, int, int, int> HexToRgba(this string source)
    {
        var rgb = formatHexString(2, true, source);
        return new Tuple<int, int, int, int>(rgb[0], rgb[1], rgb[2], rgb[3]);
    }

    private static int[] formatHexString(int numberOfBytes, bool hasAlpha, string source)
    {
        var hexColor = source.RemovePrefix("#");

        int red = 0, green = 0, blue = 0, alpha = 255;
        for (var i = 0; i < (hasAlpha ? 4 : 3) * numberOfBytes; i += numberOfBytes)
        {
            var hexPart = hexColor.Substring(i, numberOfBytes);
            var value = int.Parse(hexPart, NumberStyles.AllowHexSpecifier);

            if (numberOfBytes == 1) value = (value << 4) | value;

            switch (i / numberOfBytes)
            {
                case 0: red = value; break;
                case 1: green = value; break;
                case 2: blue = value; break;
                case 3: alpha = value; break;
            }
        }

        return [red, green, blue, alpha];
    }

    public static string CutIfTooLong(this string text, int maxLenght, bool threeDots = true)
    {
        return text.Length >= maxLenght ? $"{text.Substring(0, maxLenght)}" + (threeDots ? "..." : "") : text;
    }

    public static V? GetOrNull<K, V>(this Dictionary<K, V> dictionary, K key) where K : notnull
    {
        return dictionary.GetValueOrDefault(key);
    }

    public static T? LastOrNull<T>(this IEnumerable<T> enumerable)
    {
        var arr = enumerable.ToArray();
        return arr.Length == 0 ? default : arr.Last();
    }

    public static IEnumerable<T> Reversed<T>(this IEnumerable<T> enumerable)
    {
        var newList = enumerable.ToList();
        newList.Reverse();
        return newList;
    }
}