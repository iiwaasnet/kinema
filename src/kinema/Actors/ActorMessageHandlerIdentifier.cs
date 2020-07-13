using kinema.Core;

namespace kinema.Actors
{
    public class ActorMessageHandlerIdentifier
    {
        public IMessageIdentifier Identifier { get; set; }

        public bool KeepRegistrationLocal { get; set; }
    }
}