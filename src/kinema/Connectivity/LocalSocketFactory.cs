using System;
using System.Threading.Tasks;
using kinema.Messaging;

namespace kinema.Connectivity
{
    public class LocalSocketFactory : ILocalSocketFactory
    {
        public ILocalSocket Create(Func<IMessage, Task> receiver)
            => new LocalSocket(receiver);
    }
}