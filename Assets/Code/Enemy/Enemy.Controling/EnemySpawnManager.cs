using SpaceGame.NotImportant;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame.Enemy.Controling
{
    public class EnemySpawnManager : MonoBehaviour
    {
        private List<Transform> enemyArray = new List<Transform>();

        private void Start()
        {
            foreach (Transform child in transform)
            {
                enemyArray.Add(child);
                child.gameObject.SetActive(false);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            foreach (Transform enemy in enemyArray)
            {
                if (enemy == null)
                    return;
                enemy.gameObject.SetActive(true);
            }
        }
    }
}
