using kinema.Core.Framework;

namespace kinema.Core
{
    public class AnyVersionMessageIdentifier : MessageIdentifier
    {
        public AnyVersionMessageIdentifier(IMessageIdentifier messageMessageIdentifier)
            : this(messageMessageIdentifier.Identity)
        {
        }

        public AnyVersionMessageIdentifier(byte[] identity)
            : base(identity, 0, IdentityExtensions.Empty)
            => Identity = identity;

        protected override bool StructuralCompare(IMessageIdentifier other)
            => Identity.ArraysEqual(other.Identity);

        protected override int CalculateHashCode()
            => Identity.ComputeHash();

        public override string ToString()
            => $"{nameof(Identity)}[{Identity.GetAnyString()}]";
    }
}