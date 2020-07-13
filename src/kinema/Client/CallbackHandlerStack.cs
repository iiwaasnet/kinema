using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using kinema.Core;
using kinema.Core.Framework;

namespace kinema.Client
{
    public class CallbackHandlerStack : ICallbackHandlerStack
    {
        private readonly ConcurrentDictionary<long, IDictionary<IMessageIdentifier, IPromise>> keyPromiseMap;

        public CallbackHandlerStack()
            => keyPromiseMap = new ConcurrentDictionary<long, IDictionary<IMessageIdentifier, IPromise>>();

        public void Push(IPromise promise, IEnumerable<IMessageIdentifier> messageIdentifiers)
        {
            if (keyPromiseMap.TryAdd(promise.CallbackKey.Value, messageIdentifiers.ToDictionary(mp => mp, mp => promise)))
            {
                ((Promise) promise).SetRemoveCallbackHandler(RemoveCallback);
            }
            else
            {
                throw new DuplicatedKeyException($"Duplicated {nameof(promise.CallbackKey)} [{promise.CallbackKey.Value}]");
            }
        }

        public IPromise Pop(CallbackHandlerKey callbackIdentifier)
        {
            IPromise promise = null;

            if (keyPromiseMap.TryRemove(callbackIdentifier.CallbackKey, out var messageHandlers))
            {
                messageHandlers.TryGetValue(callbackIdentifier.MessageIdentifier, out promise);
            }

            return promise;
        }

        private void RemoveCallback(CallbackKey callbackKey)
            => keyPromiseMap.TryRemove(callbackKey.Value, out _);
    }
}