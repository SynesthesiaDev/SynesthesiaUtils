// Copyright (c) 2026 SynesthesiaDev <synesthesiadev@proton.me>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace SynesthesiaUtil.Types;

public readonly struct Snapshot<T>(T[]? array, int count) : IDisposable
{
    public readonly int Count = count;

    public Span<T> Span => array.AsSpan(0, Count);

    public void Dispose()
    {
        if (array is null || array.Length == 0) return;
        ArrayPool<T>.Shared.Return(array, clearArray: RuntimeHelpers.IsReferenceOrContainsReferences<T>());
    }
}

public static class Snapshot
{
    [SuppressMessage("Design", "MA0016:Prefer using collection abstraction instead of implementation")]
    public static Snapshot<T> Rent<T>(Span<T> list)
    {
        var count = list.Length;
        if (count == 0) return default;

        T[] arraySnapshot = ArrayPool<T>.Shared.Rent(count);

        list.CopyTo(arraySnapshot);

        return new Snapshot<T>(arraySnapshot, count);
    }
}
