// Copyright (c) 2026 SynesthesiaDev <synesthesiadev@proton.me>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Synesthesia.Utils.Randomness;

namespace Synesthesia.Utils.Extensions
{
    public static class ListExtensions
    {
        extension<T>(List<T> source)
        {
            public T Random()
            {
                return source[Rng.RandomInt(0, source.Count)];
            }

            public List<T> Filter(Predicate<T> predicate)
            {
                return source.FindAll(predicate);
            }

            public bool IsEmpty()
            {
                return source.Count == 0;
            }

            public bool IsNotEmpty()
            {
                return source.Count != 0;
            }

            public ImmutableList<T> ToImmutable()
            {
                return ImmutableList.Create(source.ToArray());
            }
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
                    select accseq.Concat([item])
            );
        }
    }
}
