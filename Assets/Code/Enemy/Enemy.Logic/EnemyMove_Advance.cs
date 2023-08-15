using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame.Enemy.Logic
{
    public class EnemyMove_Advance : MonoBehaviour
    {

        [SerializeField] private float speed;
        [SerializeField] private float waitTime;
        public Transform[] patrolPoints;

        private int currentPointIndex;
        private bool waiting;

        private void FixedUpdate()
        {
            MoveTo();
        }

        private void MoveTo()
        {
            if (waiting == true)
                return;
                
            if (transform.position != patrolPoints[currentPointIndex].position)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, speed * Time.deltaTime);
            }
            else
            {
                StartCoroutine(Wait());
            }
        }

        private IEnumerator Wait()
        {
            waiting = true;
            yield return new WaitForSeconds(waitTime);

            if (currentPointIndex + 1 < patrolPoints.Length)
            {
                currentPointIndex++;
            }
            else
            {
                currentPointIndex = 0;
            }
            waiting = false;
        }

    }
}