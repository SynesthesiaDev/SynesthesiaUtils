// Copyright (c) 2026 SynesthesiaDev <synesthesiadev@proton.me>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Collections.Immutable;

namespace SynesthesiaUtil
{
    public static class Lists
    {
        public static List<T> Of<T>() => [];

        public static List<T> Of<T>(params T[] values) => [..values];

        public static ImmutableList<T> Immutable<T>(params T[] values) => ImmutableList.Create(values);
    }
}
