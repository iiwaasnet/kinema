using System.Collections.Generic;
using kinema.Core;

namespace kinema.Messaging
{
    internal class CallbackPoint
    {
        internal IEnumerable<IMessageIdentifier> Messages { get; set; }

        internal long Key { get; set; }

        internal ReceiverIdentifier ReceiverIdentity { get; set; }

        internal NodeIdentifier ReceiverNodeIdentity { get; set; }
    }
}