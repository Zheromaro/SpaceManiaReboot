using SpaceGame.Player;
using SpaceGame.Stats.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    public class Spike : MonoBehaviour
    {
        [SerializeField] private int damage = 5; 
        [SerializeField] private bool isSandSpike;
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player"))
                return;

            Player_TroughGround player_TroughGround = collision.GetComponent<Player_TroughGround>();
            if (isSandSpike)
            {
                if (player_TroughGround._underGround == true)
                {
                    Player_Health player = collision.GetComponent<Player_Health>();
                    player.TakeDamage(damage);
                }
            }
            else
            {
                if (player_TroughGround._underGround == false)
                {
                    Player_Health player = collision.GetComponent<Player_Health>();
                    player.TakeDamage(damage);
                }
            }
        }
    }
}
