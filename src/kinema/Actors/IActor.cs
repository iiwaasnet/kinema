using System.Collections.Generic;
using kinema.Core;

namespace kinema.Actors
{
    public interface IActor
    {
        IEnumerable<MessageHandlerDefinition> GetInterfaceDefinition();

        ReceiverIdentifier Identifier { get; }
    }
}