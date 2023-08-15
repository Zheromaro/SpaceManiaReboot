using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace SpaceGame.RoomSystem
{
    public class RoomCutScene : MonoBehaviour
    {
        public PlayableDirector playableDirector;
        private string myRoom => transform.parent.parent.name;

        #region event
        private void OnEnable()
        {
            RoomExit.OnPlayerExit += ActivateCutScene;
        }

        private void OnDisable()
        {
            RoomExit.OnPlayerExit -= ActivateCutScene;
        }
        #endregion

        private void ActivateCutScene(int value)
        {
            if (myRoom == ("Room_" + value))
            {
                playableDirector.Play();
            }
        }
    }
}
