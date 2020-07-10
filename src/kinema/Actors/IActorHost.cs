namespace kinema.Actors
{
    public interface IActorHost
    {
        void AddActor(IActor actor);

        void Start();

        void Stop();
    }
}