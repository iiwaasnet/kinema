using kinema.Messaging;

namespace kinema.Connectivity
{
    public interface IReceivingConnection
    {
        void Send(IMessage message);
    }
}