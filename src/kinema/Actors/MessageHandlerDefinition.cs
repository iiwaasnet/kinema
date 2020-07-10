using kinema.Core;

namespace kinema.Actors
{
    public class MessageHandlerDefinition
    {
        public MessageHandler Handler { get; set; }

        public IMessageIdentifier Message { get; set; }

        public bool KeepRegistrationLocal { get; set; }
    }
}