using System;
using kinema.Core;

namespace kinema.Actors
{
    public class MessageHandlerDefinitionAttribute : Attribute
    {
        public MessageHandlerDefinitionAttribute(IMessageIdentifier messageIdentifier, bool keepRegistrationLocal = false)
        {
            KeepRegistrationLocal = keepRegistrationLocal;
            MessageIdentifier = messageIdentifier;
        }

        public bool KeepRegistrationLocal { get; }

        public IMessageIdentifier MessageIdentifier { get; }
    }
}