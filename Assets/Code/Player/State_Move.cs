using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using SpaceGame.Stats.Units;
using SpaceGame.SaveSystem;
using SpaceGame.Old;

namespace SpaceGame.Player
{
    public class State_Move : MonoBehaviour
    {
        private InputAction Input_Move;

        // --Movement--
        [SerializeField] private float _gravity = 10;
        [SerializeField] private float _speed;
        private Player_Health _health;
        private Rigidbody2D rb;
        private Animator animator;
        private Vector2 movement;

        // --Jump--
        [SerializeField] private float jumpForce = 10f, maximumJumpForce = 10f;
        [SerializeField] private float jumpRate = 0.2f;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private Vector2 boxSize;
        bool canJump = true;

        private void Awake()
        {
            _health = GetComponent<Player_Health>();
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            Input_Move = InputManager.inputActions.Player.Movement;
        }

        private void OnEnable()
        {
            rb.gravityScale = _gravity;
        }

        private void Update()
        {
            movement.x = Input_Move.ReadValue<Vector2>().x;
            movement.y = Input_Move.ReadValue<Vector2>().y;


            if (_health.died == false)
                animator.SetFloat("Horizontal", movement.x);
        }

        private void FixedUpdate()
        {
            rb.AddForce(new Vector2(movement.x * _speed, 0), ForceMode2D.Impulse);

            if(grounded())
                StartCoroutine(jump());
        }

        //---------------jump Functions-----------------------
        private IEnumerator jump()
        {
            if (canJump == true)
            {
                canJump = false;

                yield return new WaitForSeconds(jumpRate);

                rb.AddForce(Force(), ForceMode2D.Impulse);
                if (_health.died == false)
                    animator.Play("Player_Jump");

                yield return new WaitForSeconds(jumpRate);
                canJump = true;
            }
        }

        private Vector2 Force()
        {
            float forceAdded = ((movement.y > 0)?maximumJumpForce:0);

            if (grounded())
            {
                return (Vector2.up * jumpForce) + (Vector2.up * forceAdded);
            }

            return Vector2.zero;
        }

        private bool grounded()
        {
            if (Physics2D.OverlapBox(groundCheck.position, boxSize, 0, groundLayer))
                return true;

            return false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(groundCheck.position, boxSize);
        }
    }
}