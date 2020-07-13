using kinema.Core;
using kinema.Messaging;

namespace kinema.Client
{
    public interface IMessageHub
    {
        void SendOneWay(IMessage message);

        IPromise Send(IMessage message);

        IPromise Send(IMessage message, CallbackPoint callbackPoint);

        ReceiverIdentifier ReceiverIdentifier { get; }
    }
}