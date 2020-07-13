using System;
using System.Diagnostics.CodeAnalysis;
using kinema.Core.Framework;

namespace kinema.Core
{
    public class MessageIdentifier : IMessageIdentifier, IEquatable<IMessageIdentifier>
    {
        private int? hashCode;

        public MessageIdentifier(byte[] identity,
                                 ushort version,
                                 [AllowNull] byte[]? partition = default)
        {
            Version = version;
            Identity = identity;
            Partition = partition ?? IdentityExtensions.Empty;
        }

        public static IMessageIdentifier Create(string name, ushort version)
            => new MessageIdentifier(name.GetBytes(), version);

        internal static IMessageIdentifier CreateForFramework(string name, ushort version)
            => new MessageIdentifier($"kinema.{name}".GetBytes(), version);

        protected virtual int CalculateHashCode()
            => HashCode.Combine(Identity.ComputeHash(), Version, Partition.ComputeHash());

        public override int GetHashCode()
            => (hashCode ?? (hashCode = CalculateHashCode())).Value;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (!(obj is IMessageIdentifier))
            {
                return false;
            }
            return Equals((IMessageIdentifier) obj);
        }

        public bool Equals(IMessageIdentifier other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            return ReferenceEquals(this, other) || StructuralCompare(other);
        }

        public static bool operator ==(MessageIdentifier left, MessageIdentifier right)
        {
            if (ReferenceEquals(left, null))
            {
                return ReferenceEquals(right, null);
            }

            return left.Equals(right);
        }

        public static bool operator !=(MessageIdentifier left, MessageIdentifier right)
            => !(left == right);

        protected virtual bool StructuralCompare(IMessageIdentifier other)
            => Identity.ArraysEqual(other.Identity)
            && Version == other.Version
            && Partition.ArraysEqual(other.Partition);

        public byte[] Identity { get; protected set; }

        public ushort Version { get; }

        public byte[] Partition { get; }
    }
}