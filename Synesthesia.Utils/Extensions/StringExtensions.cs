// Copyright (c) 2026 SynesthesiaDev <synesthesiadev@proton.me>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Synesthesia.Utils.Extensions;

public static class StringExtensions
{
    extension(string source)
    {
        public int ToInt()
        {
            return int.Parse(source);
        }

        public double ToDouble()
        {
            return double.Parse(source);
        }

        public float ToFloat()
        {
            return float.Parse(source);
        }

        public byte ToByte()
        {
            return byte.Parse(source);
        }

        public short ToShort()
        {
            return short.Parse(source);
        }

        public long ToLong()
        {
            return long.Parse(source);
        }

        public bool ToBoolean()
        {
            return source.ToLower().Equals("true");
        }

        public string RemovePrefix(string prefix)
        {
            if (string.IsNullOrEmpty(prefix)) return source;

            return source.StartsWith(prefix) ? source.Substring(prefix.Length) : source;
        }

        public string RemoveSuffix(string suffix)
        {
            if (string.IsNullOrEmpty(source)) return source;

            return source.EndsWith(suffix) ? source.Substring(0, source.Length - suffix.Length) : source;
        }

        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(source);
        }

        public Tuple<int, int, int> HexToRgb()
        {
            var rgb = formatHexString(2, false, source);
            return new Tuple<int, int, int>(rgb[0], rgb[1], rgb[2]);
        }

        public Tuple<int, int, int, int> HexToRgba()
        {
            var rgb = formatHexString(2, true, source);
            return new Tuple<int, int, int, int>(rgb[0], rgb[1], rgb[2], rgb[3]);
        }

        public string CutIfTooLong(int maxLenght, bool threeDots = true)
        {
            return source.Length >= maxLenght ? $"{source.Substring(0, maxLenght)}" + (threeDots ? "..." : "") : source;
        }
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

    public static TV? GetOrNull<TK, TV>(this Dictionary<TK, TV> dictionary, TK key) where TK : notnull
    {
        return dictionary.GetValueOrDefault(key);
    }

    extension<T>(IEnumerable<T> enumerable)
    {
        public T? LastOrNull()
        {
            var arr = enumerable.ToArray();
            return arr.Length == 0 ? default : arr.Last();
        }

        public IEnumerable<T> Reversed()
        {
            var newList = enumerable.ToList();
            newList.Reverse();
            return newList;
        }
    }
}
