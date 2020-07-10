﻿using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using kinema.Core;
using kinema.Core.Framework;

namespace kinema.Actors
{
    public class ActorHandlerMap : IActorHandlerMap
    {
        private readonly ConcurrentDictionary<MessageIdentifier, MessageHandler> messageHandlers;

        public ActorHandlerMap()
            => messageHandlers = new ConcurrentDictionary<MessageIdentifier, MessageHandler>();

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

            void CleanupActorRegistrations(IEnumerable<MessageIdentifier> incomplete)
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

        private static IEnumerable<KeyValuePair<MessageIdentifier, MessageHandlerDefinition>> GetActorRegistrations(IActor actor)
            => actor.GetInterfaceDefinition()
                    .Select(messageMap =>
                                new KeyValuePair<MessageIdentifier, MessageHandlerDefinition>(new MessageIdentifier(messageMap.Message.Identity,
                                                                                                                    messageMap.Message.Version,
                                                                                                                    messageMap.Message.Partition),
                                                                                              messageMap));
    }
}