using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceGame.Enemy.Logic;

namespace SpaceGame.Enemy.Controling
{
    public class ChaseControl : MonoBehaviour
    {
        [SerializeField] private EnemyMove_Chase[] enemyArray;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            foreach (EnemyMove_Chase enemy in enemyArray)
            {
                enemy.chase = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                foreach (EnemyMove_Chase enemy in enemyArray)
                {
                    enemy.chase = false;
                }
            }
        }

    }
}
