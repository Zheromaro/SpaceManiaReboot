using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame.Enemy.Logic
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyMove_Basic : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private Vector2 movement = Vector2.zero;

        private Rigidbody2D rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            rb.velocity = new Vector2(movement.x * speed, movement.y * speed);
        }

    }
}