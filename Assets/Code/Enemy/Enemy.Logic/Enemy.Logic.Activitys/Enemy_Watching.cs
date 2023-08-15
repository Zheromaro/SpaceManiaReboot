using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame.Enemy.Logic
{
    public class Enemy_Watching : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float visionDistant;
        [SerializeField] private LineRenderer lineOfSight;

        private void Update()
        {
            lineOfSight.SetPosition(0, transform.position);

            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, visionDistant);
            if (hitInfo.collider != null)
            {
                Debug.DrawLine(transform.position, hitInfo.point, Color.yellow);
                lineOfSight.SetPosition(1, hitInfo.point);

                if (hitInfo.collider.tag == "Player")
                {

                }

            }
            else
            {
                Debug.DrawLine(transform.position, transform.position + transform.right * visionDistant, Color.green);
                lineOfSight.SetPosition(1, transform.position + transform.right * visionDistant);
            }
        }
    }
}
