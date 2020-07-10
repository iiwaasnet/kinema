using System;
using kinema.Core.Framework;

namespace kinema.Core
{
    public class ReceiverIdentifier : IEquatable<ReceiverIdentifier>
    {
        private readonly int hashCode;
        private static readonly byte[] Empty = new byte[0];

        public ReceiverIdentifier(byte[] identity, ReceiverKind receiverKind)
        {
            Identity = identity;
            ReceiverKind = receiverKind;

            hashCode = CalculateHashCode();
        }

        public static byte[] CreateIdentity()
            => Guid.NewGuid().ToString().GetBytes();

        public static ReceiverIdentifier Create(ReceiverKind receiverKind)
            => new ReceiverIdentifier(CreateIdentity(), receiverKind);

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
            return obj.GetType() == GetType()
                && StructuralCompare((ReceiverIdentifier) obj);
        }

        public static bool operator ==(ReceiverIdentifier left, ReceiverIdentifier right)
        {
            if (ReferenceEquals(left, null))
            {
                return ReferenceEquals(right, null);
            }

            return left.Equals(right);
        }

        public static bool operator !=(ReceiverIdentifier left, ReceiverIdentifier right)
            => !(left == right);

        public override int GetHashCode()
            => hashCode;

        private int CalculateHashCode()
            => Identity.ComputeHash();

        public bool Equals(ReceiverIdentifier other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return StructuralCompare(other);
        }

        public override string ToString()
            => Identity.GetAnyString();

        private bool StructuralCompare(ReceiverIdentifier other)
            => Identity.ArraysEqual(other.Identity);

        public bool IsSet()
            => Identity != null && !Identity.ArraysEqual(Empty);

        public byte[] Identity { get; }

        public ReceiverKind ReceiverKind { get; }
    }
}