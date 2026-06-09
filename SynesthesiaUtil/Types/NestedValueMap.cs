// Copyright (c) 2026 SynesthesiaDev <synesthesiadev@proton.me>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using SynesthesiaUtil.Extensions;

namespace SynesthesiaUtil.Types;

public class NestedValueMap<TK, TV> : Dictionary<TK, List<TV>> where TK : notnull
{
    public void AddValue(TK key, TV value)
    {
        if (!TryGetValue(key, out var list))
        {
            list = new List<TV>();
            this[key] = list;
        }

        list.Add(value);
    }

    public List<TV> Get(TK key)
    {
        return this.GetOrNull(key) ?? [];
    }
}
