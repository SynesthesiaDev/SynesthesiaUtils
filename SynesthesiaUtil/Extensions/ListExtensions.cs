using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using SynesthesiaUtil.Randomness;

namespace SynesthesiaUtil.Extensions
{
    public static class ListExtensions
    {
        public static T Random<T>(this List<T> source)
        {
            return source[RNG.RandomInt(0, source.Count)];
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

        public static IEnumerable<IEnumerable<T>> CartesianProduct<T>(this IEnumerable<IEnumerable<T>> sequences)
        {
            // https://stackoverflow.com/a/3098381
            IEnumerable<IEnumerable<T>> emptyProduct = [[]];
            return sequences.Aggregate(
                emptyProduct,
                (accumulator, sequence) =>
                    from accseq in accumulator
                    from item in sequence
                    select accseq.Concat(new[] { item })
            );
        }
    }
}