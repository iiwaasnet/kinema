namespace kinema.Core
{
    public interface IMessageIdentifier
    {
        ushort Version { get; }

        byte[] Identity { get; }

        byte[] Partition { get; }
    }
}