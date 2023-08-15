using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using SpaceGame.Old;

namespace SpaceGame.Player.Detection
{
    public class Detaction_RotateAimPoint : MonoBehaviour
    {
        private InputAction Input_PointerPosition;
        private Quaternion oldRotation;

        private void Update()
        {
            Vector2 direction = GetPointerPosition() - (Vector2)transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = rotation;
        }

        private void OnEnable()
        {
            Input_PointerPosition = InputManager.inputActions.Player.PointerPosition;
        }

        private Vector2 GetPointerPosition()
        {
            Vector3 mousePos = Input_PointerPosition.ReadValue<Vector2>();
            mousePos.z = Camera.main.nearClipPlane;
            return Camera.main.ScreenToWorldPoint(mousePos);
        }
    }
}
