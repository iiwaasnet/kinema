using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace kinema.Core.Framework
{
    public static class DataEncoder
    {
        private static readonly Encoding Encoder;

        static DataEncoder()
            => Encoder = Encoding.UTF8;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetAnyString(this byte[] array)
            => Encoder.GetString(array);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] GetBytes(this string str)
            => Encoder.GetBytes(str);
    }
}