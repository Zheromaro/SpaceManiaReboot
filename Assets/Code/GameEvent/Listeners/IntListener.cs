using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame.GameEvent
{
    public class IntListener : MonoBehaviour
    {
        [System.Serializable]
        class IntListeners : BaseGameEventListener<int, IntEvent, UnityIntEvent> {}

        [SerializeField] private IntListeners[] voidListeners;

        private void OnEnable()
        {
            foreach (var listener in voidListeners)
            {
                listener.OnEnable();
            }
        }

        private void OnDisable()
        {
            foreach (var listener in voidListeners)
            {
                listener.OnDisable();
            }
        }
    }
}