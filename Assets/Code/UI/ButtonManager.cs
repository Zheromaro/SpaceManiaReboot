using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using SpaceGame.SaveSystem;
using SpaceGame.Stats.Units;

namespace SpaceGame.UI
{
    public class ButtonManager : MonoBehaviour, IDataPersistence
    {
        [SerializeField] private UnitTime unitTime;
        private bool isTransitioning = false;
        private int levelReached;

        private void Awake()
        {
            SceneManager.sceneLoaded += IsTransitioningEvent;

            void IsTransitioningEvent(Scene arg0, LoadSceneMode arg1) => isTransitioning = false;
        }

        #region save
        public void LoadData(GameData data)
        {
            levelReached = data.LevelReached;
        }

        public void SaveData(GameData data)
        {
            data.LevelReached = levelReached;
        }
        #endregion

        public void NextLevel()
        {
            if (isTransitioning)
                return;

            isTransitioning = true;
            unitTime.TimeScale = 1f;

            int nextLevelNum = SceneManager.GetActiveScene().buildIndex + 1;
            SceneFader.sceneFader.FadeOut(nextLevelNum);
            levelReached = nextLevelNum;
        }

        public void NewGame()
        {
            if (isTransitioning)
                return;

            isTransitioning = true;

            // create a new game - which will initialize our game data
            DataPersistatenceManager.dataPersistatence.NewGame();

            // Load the gameplay scene - which will in turn save the game because of
            // OnSceneUnloaded() in the DataPersistatenceManager
            SceneFader.sceneFader.FadeOut("Level 1");
        }

        public void BackToMainMenu()
        {
            if (isTransitioning)
                return;

            SceneFader.sceneFader.FadeOut("Menu");
        }

        public void Restart()
        {
            if (isTransitioning)
                return;

            isTransitioning = true;

            unitTime.TimeScale = 1f;
            SceneFader.sceneFader.FadeOut(SceneManager.GetActiveScene().name);
        }


        public void QuitGame()
        {
            if (isTransitioning)
                return;

            isTransitioning = true;

            Application.Quit();
        }

    }
}