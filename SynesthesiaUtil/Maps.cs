using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace SynesthesiaUtil
{
    public static class Maps
    {

        public static Dictionary<K, V> Of<K, V>(params KeyValuePair<K, V>[] values) where K : notnull
        {
            return new Dictionary<K, V>(values);
        }

        public static Dictionary<K, V> Of<K, V>() where K : notnull
        {
            return new Dictionary<K, V>();
        }

        public static Dictionary<K, V> Of<K, V>(params (K, V)[] values) where K : notnull
        {
            return values.ToDictionary();
        }

        public static ImmutableDictionary<K, V> Immutable<K, V>(params KeyValuePair<K, V>[] values) where K : notnull
        {
            return ImmutableDictionary.CreateRange(values.AsEnumerable());
        }
    }
}