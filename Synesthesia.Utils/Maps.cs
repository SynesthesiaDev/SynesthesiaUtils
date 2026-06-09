// Copyright (c) 2026 SynesthesiaDev <synesthesiadev@proton.me>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Synesthesia.Utils
{
    public static class Maps
    {

        public static Dictionary<TK, TV> Of<TK, TV>(params KeyValuePair<TK, TV>[] values) where TK : notnull
        {
            return new Dictionary<TK, TV>(values);
        }

        public static Dictionary<TK, TV> Of<TK, TV>() where TK : notnull
        {
            return new Dictionary<TK, TV>();
        }

        public static Dictionary<TK, TV> Of<TK, TV>(params (TK, TV)[] values) where TK : notnull
        {
            return values.ToDictionary();
        }

        public static ImmutableDictionary<TK, TV> Immutable<TK, TV>(params KeyValuePair<TK, TV>[] values) where TK : notnull
        {
            return ImmutableDictionary.CreateRange(values.AsEnumerable());
        }
    }
}
