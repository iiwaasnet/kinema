using System;

namespace kinema.Actors
{
    public interface IActorHostManager : IDisposable
    {
        void AssignActor(IActor actor, ActorHostInstancePolicy actorHostInstancePolicy = ActorHostInstancePolicy.TryReuseExisting);
    }
}