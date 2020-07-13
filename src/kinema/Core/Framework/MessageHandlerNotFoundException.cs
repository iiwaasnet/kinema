using System;

namespace kinema.Core.Framework
{
    public class MessageHandlerNotFoundException : Exception
    {
        public MessageHandlerNotFoundException(IMessageIdentifier messageIdentifier)
            : base(messageIdentifier.ToString())
        {
        }
    }
}