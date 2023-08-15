using System.Collections;   
using UnityEngine;
using UnityEngine.UI;
using SpaceGame.SaveSystem;
using SpaceGame.UI;

namespace SpaceGame.UI
{
    public class LevelSelector : MonoBehaviour, IDataPersistence
    {
        [SerializeField] private Button[] levelButtons;
        private static bool isSelecting = false;

        private void Start()
        {
            if (DataPersistatenceManager.dataPersistatence.HasGameData() == false)
                transform.parent.gameObject.SetActive(false);
        }

        #region save
        public void LoadData(GameData data)
        {
            for (int i = 0; i < levelButtons.Length; i++)
            {
                if (i > data.LevelReached)
                    levelButtons[i].interactable = false;
            }

            isSelecting = false;
            transform.parent.gameObject.SetActive(false);
        }

        public void SaveData(GameData data)
        {
            // Nothing to save
        }
        #endregion

        public void Select(int i)
        {
            if(!isSelecting)
                SceneFader.sceneFader.FadeOut("Level " + i);

            isSelecting = true;
        }
    }
}
