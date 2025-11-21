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