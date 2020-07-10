using kinema.Core.Framework;

namespace kinema.Core
{
    public static class IdentityExtensions
    {
        public static readonly byte[] Empty = new byte[0];

        public static bool IsSet(this byte[] buffer)
            => !buffer.ArraysEqual(Empty);
    }
}