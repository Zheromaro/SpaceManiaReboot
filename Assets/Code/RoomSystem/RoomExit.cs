using LDtkUnity;
using SpaceGame.GameEvent;
using SpaceGame.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace SpaceGame.RoomSystem
{
    public class RoomExit : MonoBehaviour
    {
        [SerializeField] private VoidEvent _OnPlayerExit;
        public static event Action<int> OnPlayerExit;

        private LDtkFields _Fields;

        private void Start ()
        {
            _Fields = GetComponent<LDtkFields>();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player"))
                return;

            if (_Fields.TryGetInt("GoToRoom", out int value) && _Fields.TryGetEnum("ExitDiraction", out Diraction diraction))
            {
                switch (diraction)
                {
                    case Diraction.Left:
                        if (collision.transform.position.x < transform.position.x)
                            PlayerIsOut(value);
                        break;
                    case Diraction.Right:
                        if (collision.transform.position.x > transform.position.x)
                            PlayerIsOut(value);
                        break;
                    case Diraction.Up:
                        if (collision.transform.position.y > transform.position.y)
                            PlayerIsOut(value);
                        break;
                    case Diraction.Down:
                        if (collision.transform.position.y < transform.position.y)
                            PlayerIsOut(value);
                        break;
                }
            }
        }

        private void PlayerIsOut(int value)
        {
            OnPlayerExit?.Invoke(value);
            _OnPlayerExit.Raise();
        }

    }

    enum Diraction
    {
        Up,
        Down,
        Left,
        Right
    }

}
