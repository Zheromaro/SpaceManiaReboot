using UnityEngine;
using UnityEngine.UI;
using SpaceGame.GameEvent;
using SpaceGame.SaveSystem;

namespace SpaceGame
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private VoidEvent _OnNewGame;

        void Start()
        {
            if (DataPersistatenceManager.dataPersistatence.HasGameData() == false)
                _continueButton.interactable = false;

        }

        public void NewGame()
        {
            _OnNewGame.Raise();
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
