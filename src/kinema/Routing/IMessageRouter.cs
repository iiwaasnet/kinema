using System.Threading.Tasks;
using kinema.Messaging;

namespace kinema.Routing
{
    public interface IMessageRouter
    {
        Task Route(IMessage message);
    }
}