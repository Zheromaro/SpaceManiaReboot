using Cinemachine;
using SpaceGame.Player;
using SpaceGame.SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace SpaceGame.RoomSystem
{
    public class RoomSpawnPoint : MonoBehaviour, IDataPersistence
    {
        string myRoom => transform.parent.parent.name;

        public void LoadData(GameData data)
        {
            ActivateSpawnPoint(data.RoomReached);
        }

        public void SaveData(GameData data)
        {
            // nothing to save
        }

        private void ActivateSpawnPoint(int value)
        {
            if (myRoom == ("Room_" + value))
            {
                var player = GameObject.FindWithTag("Player");
                player.transform.position = transform.position;
            }
        }
    }
}
