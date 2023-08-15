using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame.Enemy.Logic
{
    public class EnemyAttack_Shooting : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float minimumDistance;
        [SerializeField] private float timeBetweenShots;
        [SerializeField] private Transform target;
        [SerializeField] private GameObject projectile;
        [SerializeField] private bool Go;

        private float nextShotTime;

        private void Start()
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        private void Update()
        {
            if (Time.deltaTime < nextShotTime)
            {
                Instantiate(projectile, transform.position, Quaternion.identity);
                nextShotTime = Time.time + timeBetweenShots;
            }

            if (Go)
            {
                if (Vector2.Distance(transform.position, target.position) > minimumDistance)
                {
                    transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                }
            }
            else
            {
                if (Vector2.Distance(transform.position, target.position) < minimumDistance)
                {
                    transform.position = Vector2.MoveTowards(transform.position, target.position, -speed * Time.deltaTime);
                }
            }
        }
    }

}