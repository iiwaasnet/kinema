﻿using System;
using System.Threading.Tasks;
using kinema.Core.Framework;
using kinema.Messaging;
using kinema.Messaging.Messages;

namespace kinema.Client
{
    internal class Promise : IPromise
    {
        private readonly TaskCompletionSource<IMessage> result;
        private volatile Action<CallbackKey> removeCallbackHandler;
        private volatile bool isDisposed;

        internal Promise(long callbackKey)
        {
            isDisposed = false;
            result = new TaskCompletionSource<IMessage>();
            CallbackKey = new CallbackKey(callbackKey);
        }

        public Task<IMessage> GetResponse()
            => !isDisposed
                   ? result.Task
                   : throw new ObjectDisposedException($"{nameof(Promise)}");

        internal void SetResult(IMessage message)
        {
            RemoveCallbackHandler();

            if (message.Equals(MessageIdentifiers.Exception))
            {
                var error = message.GetPayload<ExceptionMessage>();
                result.TrySetException(new ClientException(error.Message, error.ExceptionType, error.StackTrace));
            }
            else
            {
                result.TrySetResult(message);
            }
        }

        public void Dispose()
        {
            if (!isDisposed)
            {
                isDisposed = true;
                RemoveCallbackHandler();
            }
        }

        private void RemoveCallbackHandler()
        {
            var tmp = removeCallbackHandler;
            if (tmp != null)
            {
                removeCallbackHandler = null;
                tmp(CallbackKey);
            }
        }

        internal void SetRemoveCallbackHandler(Action<CallbackKey> removeCallbackHandler)
            => this.removeCallbackHandler = removeCallbackHandler;

        public CallbackKey CallbackKey { get; }
    }
}