using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using kinema.Core;
using kinema.Core.Framework;

namespace kinema.Actors
{
    public class ActorHandlerMap : IActorHandlerMap
    {
        private readonly ConcurrentDictionary<IMessageIdentifier, MessageHandler> messageHandlers;

        public ActorHandlerMap()
            => messageHandlers = new ConcurrentDictionary<IMessageIdentifier, MessageHandler>();

        public IEnumerable<ActorMessageHandlerIdentifier> Add(IActor actor)
        {
            var tmp = new List<ActorMessageHandlerIdentifier>();
            foreach (var reg in GetActorRegistrations(actor))
            {
                if (messageHandlers.TryAdd(reg.Key, reg.Value.Handler))
                {
                    tmp.Add(new ActorMessageHandlerIdentifier
                            {
                                Identifier = reg.Key,
                                KeepRegistrationLocal = reg.Value.KeepRegistrationLocal
                            });
                }
                else
                {
                    CleanupActorRegistrations(tmp.Select(t => t.Identifier));

                    throw new DuplicatedKeyException(reg.Key.ToString());
                }
            }

            return tmp;

            void CleanupActorRegistrations(IEnumerable<IMessageIdentifier> incomplete)
            {
                foreach (var identifier in incomplete)
                {
                    messageHandlers.TryRemove(identifier, out _);
                }
            }
        }

        public bool CanAdd(IActor actor)
            => GetActorRegistrations(actor).All(reg => !messageHandlers.ContainsKey(reg.Key));

        public MessageHandler Get(MessageIdentifier identifier)
        {
            if (messageHandlers.TryGetValue(identifier, out var value))
            {
                return value;
            }

            throw new KeyNotFoundException(identifier.ToString());
        }

        private static IDictionary<IMessageIdentifier, MessageHandlerDefinition> GetActorRegistrations(IActor actor)
            => actor.GetInterfaceDefinition()
                    .ToDictionary(mhd => mhd.Message, mhd => mhd);
    }
}