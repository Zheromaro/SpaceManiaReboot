using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame.Enemy.Logic
{
    public class EnemyMove_Chase : MonoBehaviour
    {
        [HideInInspector] public bool chase = false;
        [SerializeField] private float speed;
        
        private Transform startingPoint;
        private GameObject Player;

        private void Start()
        {
            startingPoint = gameObject.transform;
            Player = GameObject.FindGameObjectWithTag("Player");
        }

        private void Update()
        {
            if (Player == null)
                return;

            if (chase == true)
                Chase();
            else
                ReturnStartPoint();
        }

        private void Chase()
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
        }

        private void ReturnStartPoint()
        {
            transform.position = Vector2.MoveTowards(transform.position, startingPoint.position, speed * Time.deltaTime);
        }

    }
}