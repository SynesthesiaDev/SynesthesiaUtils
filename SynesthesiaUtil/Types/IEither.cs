// Copyright (c) 2026 SynesthesiaDev <synesthesiadev@proton.me>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace SynesthesiaUtil.Types
{
    public interface IEither<TL, TR>
    {
        static Left<TL, TR> Left(TL left) => new(left);

        static Right<TL, TR> Right(TR right) => new(right);
    }

    public record Left<TL, TR>(TL Value) : IEither<TL, TR>;

    public record Right<TL, TR>(TR Value) : IEither<TL, TR>;
}
