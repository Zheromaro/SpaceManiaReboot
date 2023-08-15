using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using SpaceGame.SaveSystem;
using SpaceGame.Stats.Units;
using UnityEngine.Events;

namespace SpaceGame.UI
{
    public class SceneFader : MonoBehaviour
    {
        public static SceneFader sceneFader { get; private set; }

        [SerializeField] private UnitTime _unitTime;
        [SerializeField] private int WaitFor;
        [SerializeField] private Animator animator;
        [SerializeField] private UnityEvent _OnFadeOut;

        private void Awake()
        {
            if (sceneFader != null && sceneFader != this)
            {
                Destroy(gameObject);
            }
            else
            {
                sceneFader = this;
            }
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += FadeIn;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= FadeIn;
        }

        private void FadeIn(Scene scene, LoadSceneMode mode)
        {
            _unitTime.TimeScale = 1;
            animator.Play("Fade in");
        }

        public void FadeOut(string scene)
        {
            StartCoroutine(FadeOutMangement(scene));
        }

        public void FadeOut(int i)
        {
            StartCoroutine(FadeOutMangement(i));
        }

        private IEnumerator FadeOutMangement(string scene)
        {
            _unitTime.TimeScale = 1;

            yield return new WaitForSeconds(WaitFor);
            animator.Play("Fade out");

            yield return new WaitForSeconds(1f);
            DataPersistatenceManager.dataPersistatence.SaveGame();
            SceneManager.LoadSceneAsync(scene);

            _OnFadeOut?.Invoke();
        }

        private IEnumerator FadeOutMangement(int i)
        {
            animator.Play("Fade out");
            DataPersistatenceManager.dataPersistatence.SaveGame();

            yield return new WaitForSeconds(1);

            _unitTime.TimeScale = 1;

            yield return new WaitForSeconds(WaitFor);

            SceneManager.LoadSceneAsync(i);

            _OnFadeOut?.Invoke();
        }
    }
}