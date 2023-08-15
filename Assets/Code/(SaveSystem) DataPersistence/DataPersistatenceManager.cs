using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceGame.SaveSystem
{
    public class DataPersistatenceManager : MonoBehaviour
    {
        [Header("Debugging")]
        [SerializeField] private bool initializeDataIfNull = false;

        [Header("File Storage Config")]
        [SerializeField] private string fileName;
        [SerializeField] private bool useEncryption;

        private GameData gameData;
        private List<IDataPersistence> dataPersistenceObjects;
        private FileDataHandler dataHandler;

        public static DataPersistatenceManager dataPersistatence { get; private set; }

        private void Awake()
        {
            if (dataPersistatence != null && dataPersistatence != this)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                dataPersistatence = this;
            }

            dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
        }

        #region Save&Load in

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnLoaded;
        }


        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneUnloaded -= OnSceneUnLoaded;
        }

        private void OnSceneUnLoaded(Scene arg0)
        {
            SaveGame();
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            dataPersistenceObjects = FindAllDataPersistenceObject();
            LoadGame();
        }

        private void OnApplicationQuit()
        {
            SaveGame();
        }

        #endregion

        #region Actions
        public void NewGame()
        {
            this.gameData = new GameData();
        }

        public void LoadGame()
        {
            // load any saved data from a file using the data handler
            gameData = dataHandler.Load();

            // start a new game if the data is null and we're configured to initialize data for debugging purposes
            if(gameData == null && initializeDataIfNull)
            {
                NewGame();
            }

            // if no data can be loaded, don't continue
            if(gameData == null)
            {
                Debug.Log("No data was found. A New Game needs to be started before data can be loaded.");
                return;
            }

            // push the loaded data ta all other scripts that need it
            foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
            {
                dataPersistenceObj.LoadData(gameData);
            }
        }

        public void SaveGame()
        {
            // if we don't have any data to save, log a warning here
            if (this.gameData == null)
            {
                Debug.LogWarning("No data was found. A New Game needs to be started before data can be saved.");
                return;
            }

            // pass the data to other scripts so they can update it
            foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
            {
                dataPersistenceObj.SaveData(gameData);
            }

            // save that data to a file using the data hundler
            dataHandler.Save(gameData);

        }

        #endregion

        #region Cheking
        private List<IDataPersistence> FindAllDataPersistenceObject()
        {
            IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

            return new List<IDataPersistence>(dataPersistenceObjects);

        }

        public bool HasGameData()
        {
            return gameData != null;
        }

        #endregion

    }
}
