using System.Collections.Generic;
using kinema.Messaging;

namespace kinema.Actors
{
    public interface IActorResult
    {
        IEnumerable<IMessage> Messages { get; }
    }
}