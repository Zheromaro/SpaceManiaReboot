using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using SpaceGame.Old;

namespace SpaceGame.Player.Detection
{
    public class Detaction_InGround : MonoBehaviour
    {
        [SerializeField] private LayerMask _layer;
        [SerializeField] private Vector2 _boxSize;

        public bool isInGround()
        {
            return Physics2D.OverlapBox(transform.position, _boxSize, 0, _layer);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, _boxSize);
        }
    }
}
