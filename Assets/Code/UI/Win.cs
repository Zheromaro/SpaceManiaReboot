using SpaceGame.GameEvent;
using SpaceGame.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    public class Win : MonoBehaviour
    {
        [SerializeField] private VoidEvent _OnWin;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player"))
                return;

            GetPlayerToMe(collision.gameObject);
            Invoke("PlayerWin", 1f);

        }

        private void PlayerWin() => _OnWin.Raise();

        private void GetPlayerToMe(GameObject player)
        {
            player.GetComponent<Player_Health>().DisableMovement();

            player.transform.position = transform.position;

            player.GetComponent<Animator>().Play("Player_Win");
        }
    }
}
