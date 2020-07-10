using kinema.Connectivity;
using kinema.Messaging;

namespace kinema.Routing
{
    public interface IExternalRoutingTable
    {
        IReceivingConnection FindRoute(IMessage message);
    }
}