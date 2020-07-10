using System.Collections.Generic;
using kinema.Core;

namespace kinema.Client
{
    public class CallbackPoint
    {
        public CallbackPoint(params MessageIdentifier[] messageIdentifiers)
            => MessageIdentifiers = messageIdentifiers;

        public IEnumerable<MessageIdentifier> MessageIdentifiers { get; }
    }
}