// Copyright (c) 2026 SynesthesiaDev <synesthesiadev@proton.me>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace SynesthesiaUtil.Types;

public class AtomicInt(int initialAtomicValue) : Atomic<int>(initialAtomicValue)
{
    public int Increment()
    {
        return Update(value => value + 1);
    }

    public int Decrement()
    {
        return Update(value => value - 1);
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}
