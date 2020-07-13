using System;
using System.Threading.Tasks;
using kinema.Messaging;

namespace kinema.Connectivity
{
    public class LocalSocket : ILocalSocket
    {
        private readonly Func<IMessage, Task> handler;

        public LocalSocket(Func<IMessage, Task> handler)
            => this.handler = handler;

        public Task Send(IMessage message)
            => handler(message);
    }
}