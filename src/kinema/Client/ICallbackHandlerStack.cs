using System.Collections.Generic;
using kinema.Core;

namespace kinema.Client
{
    public interface ICallbackHandlerStack
    {
        void Push(IPromise promise, IEnumerable<IMessageIdentifier> messageIdentifiers);

        IPromise Pop(CallbackHandlerKey callbackIdentifier);
    }
}