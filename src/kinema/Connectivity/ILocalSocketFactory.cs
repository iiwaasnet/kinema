using System;
using System.Threading.Tasks;
using kinema.Messaging;

namespace kinema.Connectivity
{
    public interface ILocalSocketFactory
    {
        ILocalSocket Create(Func<IMessage, Task> receiver);
    }
}