using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceGame.Enemy.Logic
{
    public class Enemy_Wave : MonoBehaviour
    {
        [SerializeField] private bool SinDirection;
        [SerializeField] private float SinLength;
        [SerializeField] private float SinSpeed;

        [Header("for Advance move")]
        [SerializeField] private Transform ParentPos;

        private Vector2 Pos;

        private void Start()
        {
            if (ParentPos == null)
            {
                Pos = transform.position;
            }
        }

        private void FixedUpdate()
        {
            WaveMove();
        }

        private void WaveMove()
        {
            Vector3 myPos = transform.position;

            if (ParentPos != null)
            {
                Pos = ParentPos.position;
            }

            if (SinDirection)
            {
                float sinY = Mathf.Sin(Time.time * SinSpeed) * SinLength;
                myPos.y = Pos.y + sinY;
            }
            else
            {
                float sinX = Mathf.Sin(Time.time * SinSpeed) * SinLength;
                myPos.x = Pos.x + sinX;
            }

            transform.position = myPos;

        }

    }
}