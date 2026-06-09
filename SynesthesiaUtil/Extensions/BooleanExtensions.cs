// Copyright (c) 2026 SynesthesiaDev <synesthesiadev@proton.me>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace SynesthesiaUtil.Extensions
{
    public static class BooleanExtensions
    {
        public static int ToInt(this bool source)
        {
            return source ? 1 : 0;
        }
    }
}
