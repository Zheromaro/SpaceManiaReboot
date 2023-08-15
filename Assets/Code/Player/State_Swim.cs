using SpaceGame.Old;
using SpaceGame.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SpaceGame
{
    public class State_Swim : MonoBehaviour
    {
        private InputAction Input_Move;

        [SerializeField] private float _speed;
        [SerializeField] private float _gravity;
        private Player_Health _health;
        private Rigidbody2D _rb;
        private Animator _animator;
        private Vector2 _movement;

        private void Awake()
        {
            _health = GetComponent<Player_Health>();
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            Input_Move = InputManager.inputActions.Player.Movement;
        }

        private void OnEnable()
        {
            _rb.gravityScale = _gravity;
            _rb.velocity = _rb.velocity / 2;
        }

        private void Update()
        {
            _movement.x = Input_Move.ReadValue<Vector2>().x;
            _movement.y = Input_Move.ReadValue<Vector2>().y;

            if (_health.died == false)
            {
                _animator.SetFloat("Horizontal", _movement.x);
                _animator.SetFloat("Vertical", _movement.y);
                _animator.SetFloat("Speed", _movement.sqrMagnitude);
            }
        }

        private void FixedUpdate()
        {
            if(_movement.x > 0.1f || _movement.y > 0.1f || _movement.x < -0.1f || _movement.y < -0.1f)
                _rb.AddForce(new Vector2(_movement.x * _speed, _movement.y * _speed), ForceMode2D.Impulse);
        }
    }
}
