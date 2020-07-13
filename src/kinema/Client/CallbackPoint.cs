using System.Collections.Generic;
using kinema.Core;

namespace kinema.Client
{
    public class CallbackPoint
    {
        private readonly HashSet<IMessageIdentifier> messageIdentifiers;

        public CallbackPoint(params IMessageIdentifier[] messageIdentifiers)
        {
            this.messageIdentifiers = new HashSet<IMessageIdentifier>(messageIdentifiers);
            if (!this.messageIdentifiers.Contains(Messaging.Messages.MessageIdentifiers.Exception))
            {
                this.messageIdentifiers.Add(Messaging.Messages.MessageIdentifiers.Exception);
            }
        }

        public IEnumerable<IMessageIdentifier> MessageIdentifiers => messageIdentifiers;
    }
}