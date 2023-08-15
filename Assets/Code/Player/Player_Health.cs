using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SpaceGame.Stats.Units;
using SpaceGame.GameEvent;
using SpaceGame.Player.Detection;

namespace SpaceGame.Player
{
    public class Player_Health : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private int _currentHealth;
        [HideInInspector] public bool died = false;
        [SerializeField] private GameObject detactionPoint;

        [Header("Events")]
        [SerializeField] private VoidEvent OnBeforePlayerDied;
        [SerializeField] private VoidEvent OnPlayerDied;
        [SerializeField] private UnityEvent _OnBeforePlayerDied, _OnPlayerDied;

        private Animator _animator;
        private Rigidbody2D _rb;
        private void Start()
        {
            _animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody2D>();
        }

        public void TakeDamage(int value)
        {
            _currentHealth -= value;

            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                BeforePlayerDied();
            }
        }

        public void BeforePlayerDied()
        {
            died = true;
            _rb.velocity = (_rb.velocity / 5);
            DisableMovement();

            _OnBeforePlayerDied?.Invoke();
            OnBeforePlayerDied.Raise();
        }

        // called by the death animation
        public IEnumerator PlayerDied()
        {
            yield return new WaitForSeconds(0.1f);
            _OnPlayerDied.Invoke();
            OnPlayerDied.Raise();
        }

        public void DisableMovement()
        {
            Player_TroughGround troughGround = GetComponent<Player_TroughGround>();
            Player_Movement movement = GetComponent<Player_Movement>();
            Player_Dash dash = GetComponent<Player_Dash>();

            troughGround.enabled = false;
            movement.enabled = false;
            dash.enabled = false;
            detactionPoint.SetActive(false);
        }

    }
}
