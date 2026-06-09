// Copyright (c) 2026 SynesthesiaDev <synesthesiadev@proton.me>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Threading;

namespace Synesthesia.Utils.Types;


public class Atomic<T>(T atomicValue) : IAtomic
{
    private readonly Lock @lock = new Lock();
    private T atomicValue = atomicValue;

    public T Value
    {
        get
        {
            lock (@lock)
            {
                return atomicValue;
            }
        }
        set
        {
            lock (@lock)
            {
                atomicValue = value;
            }
        }
    }

    public T Update(Func<T, T> updateFunction)
    {
        lock (@lock)
        {
            atomicValue = updateFunction(atomicValue);
            return atomicValue;
        }
    }

    public string GetValueAsString() => Value?.ToString() ?? "null";
}
