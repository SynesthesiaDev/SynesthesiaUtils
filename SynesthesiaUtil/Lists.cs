using System.Collections.Generic;
using System.Collections.Immutable;

namespace SynesthesiaUtil
{
    public static class Lists
    {
        public static List<T> Of<T>(params T[] values)
        {
            return new List<T>(values);
        }

        public static ImmutableList<T> Immutable<T>(params T[] values)
        {
            return ImmutableList.Create(values);
        }
    }
}