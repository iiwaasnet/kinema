using System.Collections.Generic;
using kinema.Core;

namespace kinema.Actors
{
    public interface IActorHandlerMap
    {
        IEnumerable<ActorMessageHandlerIdentifier> Add(IActor actor);

        bool CanAdd(IActor actor);

        MessageHandler Get(IMessageIdentifier identifier);
    }
}