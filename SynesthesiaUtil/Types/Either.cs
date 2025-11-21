namespace SynesthesiaUtil.Types
{
    public interface Either<L, R>
    {
        public static Left<L, R> Left(L left) => new(left);

        public static Right<L, R> Right(R right) => new(right);
    }

    public record Left<L, R>(L value) : Either<L, R>;

    public record Right<L, R>(R value) : Either<L, R>;
}