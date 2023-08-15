namespace SpaceGame.GameEvent
{
    public interface IGameEventListener<T>
    {
        void OnEventRaised(T item);
    }
}
