// Copyright (c) 2026 SynesthesiaDev <synesthesiadev@proton.me>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace Synesthesia.Utils.Extensions
{
    public static class IntExtensions
    {
        extension(int source)
        {
            public bool ToBoolean()
            {
                return source == 1;
            }

            public byte ToByte()
            {
                return Convert.ToByte(source);
            }

            public float ToFloat()
            {
                return Convert.ToDouble(source).ToFloat();
            }

            public double ToDouble()
            {
                return Convert.ToDouble(source);
            }

            public short ToShort()
            {
                return Convert.ToInt16(source);
            }

            public long ToLong()
            {
                return Convert.ToInt64(source);
            }
        }
    }
}
