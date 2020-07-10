using System;
using System.Diagnostics.CodeAnalysis;
using kinema.Core;

namespace kinema.Messaging
{
    public interface IMessage: IMessageIdentifier, IEquatable<IMessageIdentifier>
    {
        [return: MaybeNull]
        T GetPayload<T>() 
            where T : new();

        void SetReceiverNode(ReceiverIdentifier receiverNode);

        void SetReceiverActor(ReceiverIdentifier receiverNode, ReceiverIdentifier receiverActor);

        
    }
}