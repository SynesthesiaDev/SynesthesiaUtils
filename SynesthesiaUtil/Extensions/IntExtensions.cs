using System;

namespace SynesthesiaUtil.Extensions
{
    public static class IntExtensions
    {
        public static bool ToBoolean(this int source)
        {
            return source == 1;
        }

        public static byte ToByte(this int source)
        {
            return Convert.ToByte(source);
        }

        public static float ToFloat(this int source)
        {
            return Convert.ToDouble(source).ToFloat();
        }

        public static double ToDouble(this int source)
        {
            return Convert.ToDouble(source);
        }

        public static short ToShort(this int source)
        {
            return Convert.ToInt16(source);
        }
    }
}