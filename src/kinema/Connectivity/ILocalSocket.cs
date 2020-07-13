using System.Threading.Tasks;
using kinema.Messaging;

namespace kinema.Connectivity
{
    public interface ILocalSocket
    {
        Task Send(IMessage message);
    }
}