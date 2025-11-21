using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace SynesthesiaUtil.Extensions
{
    public static class ListExtensions
    {
        public static T Random<T>(this List<T> source)
        {
            var random = new Random();
            return source[random.Next(0, source.Count)];
        }

        public static List<T> Filter<T>(this List<T> source, Predicate<T> predicate)
        {
            return source.FindAll(predicate);
        }

        public static bool IsEmpty<T>(this List<T> source)
        {
            return source.Count == 0;
        }

        public static bool IsNotEmpty<T>(this List<T> source)
        {
            return source.Count != 0;
        }

        public static ImmutableList<T> ToImmutable<T>(this List<T> source)
        {
            return ImmutableList.Create<T>(source.ToArray());
        }
    }
}