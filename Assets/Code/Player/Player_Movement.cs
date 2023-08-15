using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SpaceGame.Stats.Units;

namespace SpaceGame.Player
{
    public class Player_Movement : MonoBehaviour
    {

        [SerializeField] private float _waitBeforeChangingStatefor = 0.07f;
        [SerializeField] private string[] ZeroGravityTags;
        [SerializeField] private UnityEvent OnWater, OnAir;
        private int _waterCeles = 0;
        private State_Move move;
        private State_Swim swim;
        public static bool inWater = true;

        private void Awake()
        {
            move = GetComponent<State_Move>();
            swim = GetComponent<State_Swim>();

            move.enabled = false;
            swim.enabled = false;
        }

        private void OnDisable()
        {
            move.enabled = false;
            swim.enabled = false;
        }

        private void OnEnable()
        {
            if (inWater == true)
                swim.enabled = true;
            else
                move.enabled = true;
        }

        private IEnumerator OnTriggerEnter2D(Collider2D hitInfo)
        {
            if (!InWater(hitInfo))
                yield break;

            _waterCeles++;

            if (_waterCeles == 1)
            {
                yield return new WaitForSeconds(_waitBeforeChangingStatefor);
                OnWater?.Invoke();
                move.enabled = false;
                swim.enabled = true;
                inWater = true;
            }
        }

        private IEnumerator OnTriggerExit2D(Collider2D hitInfo)
        {
            if (!InWater(hitInfo))
                yield break;

            _waterCeles--;

            if (_waterCeles <= 0)
            {
                _waterCeles = 0;

                yield return new WaitForSeconds(_waitBeforeChangingStatefor);
                OnAir?.Invoke();
                swim.enabled = false;
                move.enabled = true;
                inWater = false;
            }
        }

        private bool InWater(Collider2D hitInfo)
        {
            foreach (string tag in ZeroGravityTags)
            {
                if (hitInfo.tag == tag)
                {
                    return true;
                }
            }

            return false;
        }

    }
}
