namespace SynesthesiaUtil.Extensions
{
    public static class StringExtensions
    {
        public static int ToInt(this string source)
        {
            return int.Parse(source);
        }

        public static double ToDouble(this string source)
        {
            return double.Parse(source);
        }

        public static float ToFloat(this string source)
        {
            return float.Parse(source);
        }

        public static byte ToByte(this string source)
        {
            return byte.Parse(source);
        }

        public static short ToShort(this string source)
        {
            return short.Parse(source);
        }

        public static bool ToBoolean(this string source)
        {
            return source.ToLower().Equals("true");
        }

        public static string RemovePrefix(this string source, string prefix)
        {
            if (string.IsNullOrEmpty(prefix)) return source;

            return source.StartsWith(prefix) ? source.Substring(prefix.Length) : source;
        }

        public static string RemoveSuffix(this string source, string suffix)
        {
            if (string.IsNullOrEmpty(source)) return source;

            return source.EndsWith(suffix) ? source.Substring(0, source.Length - suffix.Length) : source;
        }

        public static bool IsEmpty(this string source)
        {
            return string.IsNullOrEmpty(source);
        }
    }
}