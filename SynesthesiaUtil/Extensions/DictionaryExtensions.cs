// Copyright (c) 2026 SynesthesiaDev <synesthesiadev@proton.me>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.Linq;

namespace SynesthesiaUtil.Extensions
{
    public static class DictionaryExtensions
    {
        extension<TK, TV>(Dictionary<TK, TV> source) where TK : notnull
        {
            public TV GetValueOrThrow(TK key)
            {
                return source[key] ?? throw new InvalidOperationException($"Dictionary does not contain a value with key {key.ToString()}");
            }

            public KeyValuePair<TK, TV> Random()
            {
                var random = new Random();
                return source.ToList()[random.Next(0, source.Count)];
            }

            public Dictionary<TK, TV> Filter(Predicate<KeyValuePair<TK, TV>> predicate)
            {
                return source.ToList().FindAll(predicate).ToDictionary();
            }
        }
    }
}
