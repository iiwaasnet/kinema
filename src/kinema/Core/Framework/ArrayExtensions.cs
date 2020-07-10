using System;

namespace kinema.Core.Framework
{
    public static class ArrayExtensions
    {
        public static int ComputeHash(this byte[] data)
            => data.Length;

        public static bool ArraysEqual(this byte[] a1, byte[] a2)
            => a1.AsSpan().SequenceEqual(a2);
    }
}