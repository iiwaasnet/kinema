using System;
using System.Threading.Tasks;
using kinema.Messaging;

namespace kinema.Client
{
    public interface IPromise : IDisposable
    {
        Task<IMessage> GetResponse();

        CallbackKey CallbackKey { get; }
    }
}