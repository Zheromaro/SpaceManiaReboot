using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using SpaceGame.Stats.Units;
using SpaceGame.Old;

namespace SpaceGame.UI
{
    public class PauseManager : MonoBehaviour
    {
        private InputAction pause;
        private InputAction resume;

        [SerializeField] private UnitTime _unitTime;
        [SerializeField] private GameObject pauseMenu;
        private bool gameIsPaused = false;
        private bool inMainMenu = false;

        #region event
        private void OnEnable()
        {
            pause = InputManager.inputActions.Player.UI_Pause;
            resume = InputManager.inputActions.UI.UI_Resume;

            pause.performed += onInput;
            resume.performed += onInput;
            SceneManager.sceneLoaded += onLoaded;
        }

        private void OnDisable()
        {
            pause.performed -= onInput;
            resume.performed -= onInput;
            SceneManager.sceneLoaded -= onLoaded;
        }

        private void onLoaded(Scene scene, LoadSceneMode arg1)
        {
            Resume();
            if (scene.name == "Menu")
                inMainMenu = true;
            else
                inMainMenu = false;
        }

        #endregion

        private void onInput(InputAction.CallbackContext obj)
        {
            if (inMainMenu == true)
                return;

            if (gameIsPaused == false)
                Pause();
            else
                Resume();
        }

        private void Pause()
        {
            pauseMenu.SetActive(true);

            InputManager.ToggeleActionMap(InputManager.inputActions.UI);
            _unitTime.TimeScale = 0;
            gameIsPaused = true;
        }

        private void Resume()
        {
            pauseMenu.SetActive(false);

            InputManager.ToggeleActionMap(InputManager.inputActions.Player);
            _unitTime.BackToOriginalMotion();
            gameIsPaused = false;
        }
    }
}