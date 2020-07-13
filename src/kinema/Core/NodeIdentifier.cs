using System;
using kinema.Core.Framework;

namespace kinema.Core
{
    public class NodeIdentifier : IEquatable<NodeIdentifier>
    {
        private readonly int hashCode;
        private static readonly byte[] Empty = new byte[0];

        private NodeIdentifier(byte[] identity, NodeType nodeType)
        {
            Identity = identity;
            NodeType = nodeType;

            hashCode = CalculateHashCode();
        }

        public static NodeIdentifier Local()
            => new NodeIdentifier(Empty, NodeType.Local);

        public static NodeIdentifier Remote(byte[] identity)
            => new NodeIdentifier(identity, NodeType.Remote);

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
                && StructuralCompare((NodeIdentifier) obj);
        }

        public static bool operator ==(NodeIdentifier left, NodeIdentifier right)
        {
            if (ReferenceEquals(left, null))
            {
                return ReferenceEquals(right, null);
            }

            return left.Equals(right);
        }

        public static bool operator !=(NodeIdentifier left, NodeIdentifier right)
            => !(left == right);

        public override int GetHashCode()
            => hashCode;

        private int CalculateHashCode()
            => Identity.ComputeHash();

        public bool Equals(NodeIdentifier other)
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

        private bool StructuralCompare(NodeIdentifier other)
            => Identity.ArraysEqual(other.Identity);

        public bool IsSet()
            => Identity != null && !Identity.ArraysEqual(Empty);

        public byte[] Identity { get; }

        public NodeType NodeType { get; }
    }
}