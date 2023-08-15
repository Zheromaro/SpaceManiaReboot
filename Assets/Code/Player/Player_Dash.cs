using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System.Collections;
using SpaceGame.Stats.Units;
using SpaceGame.GameEvent;
using SpaceGame.Old;

namespace SpaceGame.Player
{
    public class Player_Dash : MonoBehaviour
    {
        private InputAction Input_Move;
        private InputAction Input_Dash;

        [Header("Dash valus")]
        [SerializeField] private float _dashingVelocity = 14f;
        [SerializeField] private float _dashingTime = 0.5f;
        [SerializeField] private int _dashTimes = 1;
        private Rigidbody2D _rb;
        private Coroutine _coroutine;
        public int DashTimes
        {
            get { return _dashTimes; }
            set
            {
                _dashTimes = value;

                if (_dashTimes <= 0)
                {
                    _dashTimes = 0;
                    _spriteRenderer.color = _cantDashColor;
                }
                else
                {
                    _spriteRenderer.color = _canDashColor;
                }
            }
        }

        [Header("Effect")]
        [SerializeField] private Color _canDashColor;
        [SerializeField] private Color _cantDashColor;
        private SpriteRenderer _spriteRenderer;

        [Header("Event")]
        [SerializeField] private VoidEvent __Dashing;
        [SerializeField] private VoidEvent __DashingEnd;
        [SerializeField] private UnityEvent _Dashing, _DashingEnd;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        #region event
        private void OnEnable()
        {
            Input_Move = InputManager.inputActions.Player.Movement;
            Input_Dash = InputManager.inputActions.Player.Dash;

            Input_Dash.performed += DashEvent;
        }

        private void OnDisable()
        {
            Input_Dash.performed -= DashEvent;

            if (_coroutine != null)
                StopCoroutine(_coroutine);
        }

        public void DashEvent(InputAction.CallbackContext obj) => _coroutine = StartCoroutine(Dash());
        #endregion

        private IEnumerator Dash()
        {
            // --- Dashing start ------------------------------------------------------------------------------------------
            Vector2 dashingDir = new Vector2(Input_Move.ReadValue<Vector2>().x, Input_Move.ReadValue<Vector2>().y).normalized;

            if (DashTimes <= 0 || dashingDir == Vector2.zero)
                yield break;

            if(Player_Movement.inWater == false)
                DashTimes -= 1;

            // --- Dashing ------------------------------------------------------------------------------------------------

            _Dashing?.Invoke();
            __Dashing.Raise();

            _rb.gravityScale = 0;

            float _currentDashingTime = 0;
            while (_dashingTime > _currentDashingTime)
            {
                _currentDashingTime += Time.unscaledTime;
                _rb.velocity = dashingDir * _dashingVelocity;
                yield return null;
            }

            // --- Dashing end --------------------------------------------------------------------------------------------

            _DashingEnd?.Invoke();
            __DashingEnd.Raise();
        }
    }
}
