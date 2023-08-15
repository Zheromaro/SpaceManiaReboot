using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame.Enemy.Logic
{
    public class Enemy_LookAtPlayer : MonoBehaviour
    {
        private GameObject Player;

        private void Start()
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }

        private void Update()
        {
            if (Player == null)
                return;

            Flip();
        }

        private void Flip()
        {
            if (transform.position.x > Player.transform.position.x)
                transform.rotation = Quaternion.Euler(0, 0, 0);
            else
                transform.rotation = Quaternion.Euler(0, 180, 0);
        }

    }
}