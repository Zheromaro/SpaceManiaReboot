using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using SpaceGame.Player.Detection;
using SpaceGame.Stats.Units;
using SpaceGame.GameEvent;
using SpaceGame.Old;
using static UnityEngine.EventSystems.StandaloneInputModule;

namespace SpaceGame.Player
{
    public class Player_TroughGround : MonoBehaviour
    {
        private InputAction Input_TroughGround;

        [Header("Valus")]
        [SerializeField] private float _outSpeed;
        [SerializeField] private float _waitInBetween;
        [SerializeField] private float _inSpeed;
        [SerializeField] private float _outGroundForce = 50f;
        public bool _underGround;
        private Animator _animator;
        private Rigidbody2D _rb;
        private Coroutine _coroutine;
        private Detaction_InGround _detactionInGround;
        private Detaction_Line _detactionLine;
        private Player_Health _health;

        [Header("Event")]
        public VoidEvent __OnGoingToGround;
        public VoidEvent __OnTouchingGround, __OnThroughGround, __OnOutGround, __OnEnd;
        [SerializeField] private UnityEvent _OnGoingToGround, _OnTouchingGround, _OnThroughGround, _OnOutGround, _OnEnd;


        private void Start()
        {
            _detactionLine = GetComponentInChildren<Detaction_Line>();
            _detactionInGround = GetComponentInChildren<Detaction_InGround>();

            _health = GetComponent<Player_Health>();
            _animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody2D>();
        }

        #region event
        private void OnEnable()
        {
            Input_TroughGround = InputManager.inputActions.Player.GoThroughGround;

            Input_TroughGround.performed += AcendEvent;
        }

        private void OnDisable()
        {
            Input_TroughGround.performed -= AcendEvent;

            if (_coroutine != null)
                StopCoroutine(_coroutine);
        }

        public void AcendEvent(InputAction.CallbackContext obj) => _coroutine = StartCoroutine(Acend());
        #endregion

        private IEnumerator Acend()
        {
            Vector2 myDirection = _detactionLine.direction;
            Vector2 myPoint = _detactionLine.pointDetacted;

            #region Test
            if (_underGround == true || _health.died == true)
                yield break;

            if (_detactionLine.pointDetacted == Vector2.zero)
            {
                Debug.LogWarning("There is no point detacted");
                yield break;
            }

            if (_detactionLine.direction == Vector2.zero)
            {
                Debug.LogWarning("There is no direction to go");
                yield break;
            }
            #endregion

            yield return StartCoroutine(GoToAcendPoint(myDirection, myPoint));

            yield return StartCoroutine(Wait());

            yield return StartCoroutine(DoAcend(myDirection));

            GetOut(myDirection);

            // End
            __OnEnd.Raise();
            _OnEnd?.Invoke();
        }

        //-------------------------------------------------------------------------

        private IEnumerator GoToAcendPoint(Vector2 myDirection, Vector2 myPoint)
        {
            __OnGoingToGround.Raise();
            _OnGoingToGround?.Invoke();

            _underGround = true;
            _rb.velocity = Vector2.zero;

            float distance = Vector2.Distance(transform.position, myPoint);
            while (distance > 0.3f)
            {
                transform.Translate(myDirection * _inSpeed * Time.unscaledDeltaTime);

                distance = Vector2.Distance(transform.position, myPoint);
                yield return null;
            }

            while (_detactionInGround.isInGround() == false)
            {
                transform.Translate(myDirection * _inSpeed * Time.unscaledDeltaTime);

                distance = Vector2.Distance(transform.position, myPoint);
                if (distance > 0.4f)
                    yield break;

                yield return null;
            }
        }

        private IEnumerator Wait()
        {
            __OnTouchingGround.Raise();
            _OnThroughGround?.Invoke();

            yield return new WaitForSeconds(_waitInBetween);
        }

        private IEnumerator DoAcend(Vector2 myDirection)
        {
            __OnThroughGround.Raise();
            _OnThroughGround?.Invoke();

            if(_health.died == false)
                _animator.Play("Player_DivingUnderGround");

            while (_detactionInGround.isInGround() == true)
            {
                transform.Translate(myDirection * _inSpeed * Time.deltaTime);

                yield return null;
            }
        }

        private void GetOut(Vector2 myDirection)
        {
            __OnOutGround.Raise();
            _OnOutGround?.Invoke();

            _rb.velocity = Vector2.zero;
            _rb.AddForce(myDirection * _outGroundForce, ForceMode2D.Impulse);
            _animator.SetTrigger("isOut");

            _detactionLine.direction = Vector2.zero;
            _detactionLine.pointDetacted = Vector2.zero;
            _underGround = false;
        }

    }
}
