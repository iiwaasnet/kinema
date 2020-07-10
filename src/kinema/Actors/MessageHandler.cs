using System.Threading.Tasks;
using kinema.Messaging;

namespace kinema.Actors
{
    public delegate Task<IActorResult> MessageHandler(IMessage message);
}