using System;
using System.Collections.Generic;
using System.Linq;

namespace SynesthesiaUtil.Extensions
{
    public static class EnumExtensions
    {
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
    }
}