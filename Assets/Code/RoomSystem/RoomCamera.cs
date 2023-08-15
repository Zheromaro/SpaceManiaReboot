using Cinemachine;
using LDtkUnity;
using SpaceGame.Player;
using SpaceGame.SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace SpaceGame.RoomSystem
{
    public class RoomCamera : MonoBehaviour, IDataPersistence
    {
        private CinemachineConfiner2D m_Confiner;
        private CinemachineVirtualCamera m_VirtualCamera;
        private LDtkFields l_Fields;
        private string myRoom => transform.parent.parent.name;

        private void Awake()
        {
            // ---- Getting Components ----------------
            m_Confiner = GetComponent<CinemachineConfiner2D>();
            m_VirtualCamera = GetComponent<CinemachineVirtualCamera>();
            l_Fields = GetComponent<LDtkFields>();

        }

        private void Start()
        {
            // ---- Setting the camera ----------------

            // Camera boundres
            m_Confiner.m_BoundingShape2D = transform.parent.parent.gameObject.GetComponent<PolygonCollider2D>();

            // Camera Target
            if(l_Fields.TryGetBool("fallow_Player", out bool value))
            {
                if(value == true)
                {
                    var player = GameObject.FindWithTag("Player");
                    m_VirtualCamera.Follow = player.transform;
                }
            }

            // Camera Size
            m_VirtualCamera.m_Lens.OrthographicSize = (transform.lossyScale.x * 0.28f);
        }

        #region event
        private void OnEnable()
        {
            RoomExit.OnPlayerExit += ActivateCamera;
        }

        private void OnDisable()
        {
            RoomExit.OnPlayerExit -= ActivateCamera;
        }
        #endregion

        #region save
        public void LoadData(GameData data)
        {
            ActivateCamera(data.RoomReached);
        }

        public void SaveData(GameData data)
        {
            // nothing to save
        }
        #endregion

        private void ActivateCamera(int value)
        {
            if (myRoom == ("Room_" + value))
            {
                m_VirtualCamera.enabled = true;
            }
            else
            {
                m_VirtualCamera.enabled = false;
            }
        }
    }
}
