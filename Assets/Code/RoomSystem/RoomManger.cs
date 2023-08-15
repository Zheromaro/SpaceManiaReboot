using System.Collections.Generic;
using UnityEngine;
using LDtkUnity;
using SpaceGame.GameEvent;
using SpaceGame.SaveSystem;
using static UnityEngine.Rendering.DebugUI;
using SpaceGame.Old;

namespace SpaceGame.RoomSystem
{
    public class RoomManger : MonoBehaviour, IDataPersistence
    {
        private int currentNumberRoom;

        #region event
        public int CurrentNumberRoom
        {
            get
            {
                return currentNumberRoom;
            }
            set
            {
                currentNumberRoom = value;
            }
        }  // I did this so I can use it in unity events

        private void OnEnable()
        {
            RoomExit.OnPlayerExit += EventValue;
        }

        private void OnDisable()
        {
            RoomExit.OnPlayerExit -= EventValue;
        }

        private void EventValue(int value) => SwitchToRoom(value);
        #endregion

        #region save
        public void LoadData(GameData data)
        {

            foreach (Transform t in transform)
            {
                if (t.name == ("Room_" + data.RoomReached))
                {
                    currentNumberRoom = data.RoomReached;
                }
            }

            SwitchToRoom(currentNumberRoom, true);
        }

        public void SaveData(GameData data)
        {
            data.RoomReached = currentNumberRoom;
        }

        #endregion

        private void SwitchToRoom(int newNumberRoom, bool CheckAllRooms = false)
        {
            // Get the new rooms
            List<int> newRooms = SurroundingRoom(newNumberRoom);
            newRooms.Add(newNumberRoom);

            // Get the old rooms
            List<int> oldRooms = new List<int>();

            if (CheckAllRooms)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    oldRooms.Add(i);
                }
            }
            else
            {
                oldRooms = SurroundingRoom(currentNumberRoom);
                oldRooms.Add(currentNumberRoom);
            }

            // ---- Activating newRooms -------------------------------------------------------------------------------------
            foreach (int room in newRooms)
            {
                transform.Find("Room_" + room).gameObject.SetActive(true);

                if (oldRooms.Contains(room))
                    oldRooms.Remove(room);
            }

            // ---- Deactivating oldRooms -----------------------------------------------------------------------------------
            foreach (int room in oldRooms)
            {
                transform.Find("Room_" + room).gameObject.SetActive(false);
            }

            // ---- Switch Current Room -------------------------------------------------------------------------------------
            currentNumberRoom = newNumberRoom;
        }

        List<int> SurroundingRoom(int roomNumber)
        {
            GameObject room = transform.Find("Room_" + roomNumber).gameObject;

            List<int> surroundingRoom = new List<int>();
            foreach (Transform child in (room.transform.Find("Objects").transform))
            {
                RoomExit component = child.GetComponent<RoomExit>();
                if (component != null)
                {
                    if (child.GetComponent<LDtkFields>().TryGetInt("GoToRoom", out int value))
                    {
                        surroundingRoom.Add(value);
                    }
                }
            }

            return surroundingRoom;
        }

    }
}
