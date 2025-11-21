using System;
using System.Collections.Generic;
using System.Linq;

namespace SynesthesiaUtil.Extensions
{
    public static class DictionaryExtensions
    {
        public static V GetValueOrThrow<K, V>(this Dictionary<K, V> source, K key) where K : notnull
        {
            return source[key] ?? throw new InvalidOperationException($"Dictionary does not contain a value with key {key.ToString()}");
        }

        public static KeyValuePair<K, V> Random<K, V>(this Dictionary<K, V> source) where K : notnull
        {
            var random = new Random();
            return source.ToList()[random.Next(0, source.Count)];
        }

        public static Dictionary<K, V> Filter<K, V>(this Dictionary<K, V> source, Predicate<KeyValuePair<K, V>> predicate) where K : notnull
        {
            return source.ToList().FindAll(predicate).ToDictionary();
        }
    }
}