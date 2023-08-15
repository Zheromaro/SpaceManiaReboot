using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame.GameEvent
{
    public class VoidListener : MonoBehaviour
    {
        [System.Serializable]
        class VoidListeners : BaseGameEventListener<Void, VoidEvent, UnityVoidEvent> {}

        [SerializeField] private VoidListeners[] voidListeners;

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