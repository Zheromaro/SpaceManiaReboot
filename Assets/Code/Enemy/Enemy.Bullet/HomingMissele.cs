using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame.Enemy.Bullet
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class HomingMissele : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
        [SerializeField] private float rotateSpeed = 200f;
        private Transform target;
        private Rigidbody2D rb;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        void FixedUpdate()
        {
            Vector2 direction = (Vector2)target.position - rb.position;

            direction.Normalize();

            float rotateAmount = Vector3.Cross(direction, transform.up).z;

            rb.angularVelocity = -rotateAmount * rotateSpeed;

            rb.velocity = transform.up * speed;
        }
    }
}