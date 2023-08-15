using UnityEngine;
using UnityEngine.Events;

namespace SpaceGame.GameEvent
{
    public abstract class BaseGameEventListener<T, E, UER> :
        IGameEventListener<T> where E : BaseGameEvent<T> where UER : UnityEvent<T>
    {
        [SerializeField] private E gameEvent;

        public E GameEvent { get { return gameEvent; } set { gameEvent = value; } }

        [SerializeField] private UER unityEventResponse;

        public void OnEnable()
        {
            if (gameEvent == null) { return; }

            GameEvent.RegisterListener(this);
        }

        public void OnDisable()
        {
            if (gameEvent == null) { return; }

            GameEvent.UnregisterListener(this);
        }

        public void OnEventRaised(T item)
        {
            if (unityEventResponse != null)
            {
                unityEventResponse.Invoke(item);
            }
        }
    }
}