using UnityEngine;
using System.Collections;
using SpaceGame.Old;

namespace SpaceGame.Stats.Units
{
    [CreateAssetMenu(fileName = "New UnitTime", menuName = "GameUnits/UnitTime")]
    public class UnitTime : ScriptableObject
    {
        // Fields
        [SerializeField] private float _slowedTime = 0.05f;
        [SerializeField] private float _normalTimeToSlow = 1f;
        [SerializeField] private float _slowTimeToNormal = 1f;

        private Coroutine _slowCoroutine;
        private Coroutine _normalCoroutine;
        private bool _normalMotion = true;
        private float _remeberTimeScale = 1;

        // Properties

        public float TimeScale
        {
            get
            {
                return Time.timeScale;
            }
            set
            {
                _remeberTimeScale = Time.timeScale;

                if (_normalCoroutine != null)
                {
                    Managers.managers.StopCoroutine(_normalCoroutine);
                }

                if (_slowCoroutine != null)
                {
                    Managers.managers.StopCoroutine(_slowCoroutine);
                }

                Time.timeScale = value;
            }
        }

        // Methods

        public void BackToOriginalMotion()
        {
            Time.timeScale = _remeberTimeScale;

            if (_normalMotion)
            {
                _normalCoroutine = Managers.managers.StartCoroutine(NormalMotionCoroutine());
            }
            else
            {
                _slowCoroutine = Managers.managers.StartCoroutine(SlowMotionCoroutine());
            }
        }

        public void SlowMotion()
        {
            if (_normalCoroutine != null)
            {
                Managers.managers.StopCoroutine(_normalCoroutine);
            }

            _slowCoroutine = Managers.managers.StartCoroutine(SlowMotionCoroutine());
        }

        public void NormalMotion()
        {
            if (_slowCoroutine != null)
            {
                Managers.managers.StopCoroutine(_slowCoroutine);
            }

            _normalCoroutine = Managers.managers.StartCoroutine(NormalMotionCoroutine());
        }

        IEnumerator SlowMotionCoroutine()
        {
            _normalMotion = false;

            while (Time.timeScale >= (_slowedTime + (1f / _normalTimeToSlow) * Time.unscaledDeltaTime)) 
            {
                Time.timeScale -= (1f / _normalTimeToSlow) * Time.unscaledDeltaTime;
                Time.fixedDeltaTime = Time.timeScale * 0.02f;
                yield return null;
            }

            Time.timeScale = _slowedTime;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;

        }

        IEnumerator NormalMotionCoroutine()
        {
            _normalMotion = true;

            while (Time.timeScale <= 1f)
            {
                Time.timeScale += (1f / _slowTimeToNormal) * Time.unscaledDeltaTime;
                Time.fixedDeltaTime = Time.timeScale * 0.02f;
                yield return null;
            }

            Time.timeScale = 1;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;

        }

    }
}