using System;
using System.Diagnostics.CodeAnalysis;
using kinema.Core;

namespace kinema.Messaging
{
    public interface IMessage : IMessageIdentifier, IEquatable<IMessageIdentifier>
    {
        [return: MaybeNull]
        T GetPayload<T>()
            where T : new();

        //void SetReceiverNode(ReceiverIdentifier receiverNode);

        //void SetReceiverActor(ReceiverIdentifier receiverNode, ReceiverIdentifier receiverActor);
        internal CallbackPoint CallbackPoint { get; set; }

        internal void RegisterCallbackPoint(CallbackPoint callbackPoint);

        internal IMessage CopyMessageProperties(IMessage src);

        byte[] CorrelationId { get; internal set; }

        DistributionPattern Distribution { get; internal set; }

        string Domain { get; internal set; }
    }
}