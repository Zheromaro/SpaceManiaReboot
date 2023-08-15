using LDtkUnity;
using SpaceGame.Player;
using SpaceGame.Stats.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.SceneManagement;

namespace SpaceGame
{
    public class RoomSet : MonoBehaviour
    {
        #region TileCollisions
        [SerializeField] private string[] TriggerTags;

        void Start()
        {
            void SetTileCollisions()
            {
                // Getting layers
                List<LDtkComponentLayer> IntGridLayers = new List<LDtkComponentLayer>();
                foreach (Transform child in transform)
                {
                    if (child.TryGetComponent<LDtkComponentLayer>(out LDtkComponentLayer componentLayer))
                    {
                        if (componentLayer.LayerType.ToString() == "IntGrid")
                        {
                            IntGridLayers.Add(componentLayer);
                        }
                    }
                }

                // Setting layers collisions
                foreach (LDtkComponentLayer layer in IntGridLayers)
                {
                    foreach (Transform tilemap in layer.transform)
                    {
                        if (tilemap.TryGetComponent<CompositeCollider2D>(out CompositeCollider2D compositeCollider2D))
                        {
                            compositeCollider2D.geometryType = CompositeCollider2D.GeometryType.Polygons;

                            // checking for tags
                            foreach (string tag in TriggerTags)
                            {
                                if (tilemap.gameObject.tag == tag)
                                {
                                    compositeCollider2D.isTrigger = true;
                                }
                            }
                        }
                    }
                }

            }

            SetTileCollisions();
        }

        #endregion

        private static int playerIsInRooms = 0;

        #region sceneEvent
        private void OnEnable()
        {
            SceneManager.sceneLoaded += ResetPlayerIsInRoom;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= ResetPlayerIsInRoom;
        }

        private void ResetPlayerIsInRoom(Scene arg0, LoadSceneMode arg1) => playerIsInRooms = 0;

        #endregion

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player"))
                return;

            playerIsInRooms += 1;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player"))
                return;

            playerIsInRooms -= 1;

            if (playerIsInRooms <= 0)
            {
                playerIsInRooms = 0;

                Player_Health player = collision.GetComponent<Player_Health>();
                player.TakeDamage(100000);
            }
        }


    }
}
