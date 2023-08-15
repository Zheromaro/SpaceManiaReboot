using UnityEngine;

namespace SpaceGame.SaveSystem
{
    [System.Serializable]
    public class GameData
    {
        public int RoomReached;
        public int LevelReached;

        // the values defined in this constructor will be the default values
        // the game starts with when there's no data to load
        public GameData()
        {
            RoomReached = 0;
            LevelReached = 0;
        }

    }
}
