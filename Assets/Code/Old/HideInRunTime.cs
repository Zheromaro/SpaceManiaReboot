using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class HideInRunTime : MonoBehaviour
    {
        private void Start()
        {
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();

            sprite.enabled = false;
        }
    }
}
