using kinema.Connectivity;
using kinema.Core;

namespace kinema.Routing
{
    public class InternalRouteRegistration
    {
        public ReceiverIdentifier ReceiverIdentifier { get; set; }

        public bool KeepRegistrationLocal { get; set; }

        public IMessageIdentifier? MessageContract { get; set; }

        public ILocalSocket DestinationSocket { get; set; }
    }
}