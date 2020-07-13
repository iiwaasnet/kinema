using System;
using System.Linq;
using System.Threading.Tasks;
using kinema.Connectivity;
using kinema.Core.Framework;
using kinema.Messaging;
using kinema.Messaging.Messages;
using kinema.Messaging.Messages.Extensions;
using kinema.Routing;
using kinema.Security;

namespace kinema.Actors
{
    public class ActorHost : IActorHost
    {
        private readonly IInternalRegistrationService internalRegistrationService;
        private readonly IActorHandlerMap actorHandlerMap;
        private readonly IMessageRouter messageRouter;
        private readonly ISecurityProvider securityProvider;
        private readonly ILocalSocket socket;

        public ActorHost(IInternalRegistrationService internalRegistrationService,
                         IActorHandlerMap actorHandlerMap,
                         ILocalSocketFactory localSocketFactory,
                         IMessageRouter messageRouter,
                         ISecurityProvider securityProvider)
        {
            this.internalRegistrationService = internalRegistrationService;
            this.actorHandlerMap = actorHandlerMap;
            this.messageRouter = messageRouter;
            this.securityProvider = securityProvider;
            socket = localSocketFactory.Create(DispatchMessage);
        }

        private async Task DispatchMessage(IMessage messageIn)
        {
            var messageHandler = actorHandlerMap.Get(messageIn);
            if (messageHandler != null)
            {
                try
                {
                    var response = (await messageHandler(messageIn)).Messages;

                    foreach (var messageOut in response)
                    {
                        messageRouter.Route(SetOutMessageProperties(messageOut));
                    }
                }
                catch (Exception err)
                {
                    var payload = err.BuildExceptionMessage();

                    var messageOut = Message.Create(payload, MessageIdentifiers.Exception);
                    messageRouter.Route(SetOutMessageProperties(messageOut));
                }
            }
            else
            {
                throw new MessageHandlerNotFoundException(messageIn);
            }

            IMessage SetOutMessageProperties(IMessage messageOut)
            {
                messageOut.Domain = securityProvider.GetDomain(messageOut.Identity);

                return messageOut.CopyMessageProperties(messageIn);
            }
        }

        public void AddActor(IActor actor)
        {
            var registrations = actorHandlerMap.Add(actor);
            internalRegistrationService.RegisterInternalRoutes(registrations.Select(r => new InternalRouteRegistration
                                                                                         {
                                                                                             DestinationSocket = socket,
                                                                                             KeepRegistrationLocal = r.KeepRegistrationLocal,
                                                                                             ReceiverIdentifier = actor.Identifier,
                                                                                             MessageContract = r.Identifier
                                                                                         }
                                                                                   ));
        }
    }
}