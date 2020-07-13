using System;
using System.Threading;
using System.Threading.Tasks;
using kinema.Connectivity;
using kinema.Core;
using kinema.Messaging;
using kinema.Messaging.Messages;
using kinema.Routing;
using kinema.Security;

namespace kinema.Client
{
    public class MessageHub : IMessageHub
    {
        private readonly IInternalRegistrationService internalRegistrationService;
        private readonly IMessageRouter messageRouter;
        private readonly ISecurityProvider securityProvider;
        private readonly ICallbackHandlerStack callbackHandlers;
        private readonly ILocalSocket socket;
        private long lastCallbackKey = 0;
        private static readonly CallbackPoint ReceiptConfirmationCallbackPoint = new CallbackPoint(MessageIdentifiers.ReceiptConfirmation);

        public MessageHub(IInternalRegistrationService internalRegistrationService,
                          ILocalSocketFactory localSocketFactory,
                          IMessageRouter messageRouter,
                          ISecurityProvider securityProvider,
                          ICallbackHandlerStack callbackHandlers)
        {
            ReceiverIdentifier = ReceiverIdentifier.Create(ReceiverKind.MessageHub);
            this.internalRegistrationService = internalRegistrationService;
            this.messageRouter = messageRouter;
            this.securityProvider = securityProvider;
            this.callbackHandlers = callbackHandlers;
            socket = localSocketFactory.Create(DispatchMessage);
        }

        private Task DispatchMessage(IMessage arg)
            => throw new NotImplementedException();

        public void SendOneWay(IMessage message)
        {
            message.Domain = securityProvider.GetDomain(message.Identity);
            messageRouter.Route(message);
        }

        public IPromise SendWithReceiptAck(IMessage message)
            => Send(message, ReceiptConfirmationCallbackPoint);

        public IPromise Send(IMessage message, CallbackPoint callbackPoint)
        {
            message.Domain = securityProvider.GetDomain(message.Identity);
            var promise = new Promise(Interlocked.Increment(ref lastCallbackKey));

            message.RegisterCallbackPoint(new Messaging.CallbackPoint
                                          {
                                              Key = promise.CallbackKey.Value,
                                              Messages = callbackPoint.MessageIdentifiers,
                                              ReceiverIdentity = ReceiverIdentifier,
                                              ReceiverNodeIdentity = NodeIdentifier.Local()
                                          });
            callbackHandlers.Push(promise, callbackPoint.MessageIdentifiers);

            messageRouter.Route(message);

            return promise;
        }

        private static void AssertMessageIsNotMulticast(IMessage message)
        {
            if (message.Distribution == DistributionPattern.Multicast)
            {
                throw new Exception($"{nameof(DistributionPattern.Multicast)} message can't be sent with receipt confirmation!");
            }
        }

        public ReceiverIdentifier ReceiverIdentifier { get; }
    }
}