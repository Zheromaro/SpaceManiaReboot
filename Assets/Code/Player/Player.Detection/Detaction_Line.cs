using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using SpaceGame.Stats.Units;
using SpaceGame.Old;

namespace SpaceGame.Player.Detection
{
    public class Detaction_Line : MonoBehaviour
    {
        private InputAction Input_goThroughGround;
        [SerializeField] private UnitTime unitTime;

        // ---rayDetection---
        [SerializeField] private LayerMask layerDetection;
        [SerializeField] private float lineLength;
        private LineRenderer _lineRenderer;
        private Coroutine myCoroutine;

        // ---Resute :> ---
        [HideInInspector] public Vector2 direction = Vector2.zero;
        [HideInInspector] public Vector2 pointDetacted = Vector2.zero;

        private void Start ()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }

        #region calling the function
        private void OnEnable()
        {
            Input_goThroughGround = InputManager.inputActions.Player.DetectGround;
            Input_goThroughGround.performed += StartChecking;
            Input_goThroughGround.canceled += EndChecking;
        }

        private void OnDisable()
        {
            Input_goThroughGround.performed -= StartChecking;
            Input_goThroughGround.canceled -= EndChecking;
            _lineRenderer.enabled = false;

            if(myCoroutine != null)
                StopCoroutine(myCoroutine);
        }

        private void StartChecking(InputAction.CallbackContext obj)
        {
            unitTime.SlowMotion();
            _lineRenderer.enabled = true;
            myCoroutine = StartCoroutine(CheckForPoint());
        }

        public void EndChecking(InputAction.CallbackContext obj)
        {
            unitTime.NormalMotion();
            _lineRenderer.enabled = false;
            if(myCoroutine != null)
                StopCoroutine(myCoroutine);
        } 

        #endregion

        private IEnumerator CheckForPoint()
        {
            // -------- Detacting collision -------------------------------------------------------------------------------

            Vector3 endPosition = transform.position + (transform.right * lineLength);
            _lineRenderer.SetPositions(new Vector3[] { transform.position, endPosition });
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, lineLength, layerDetection);

            // -------- Finding collision ---------------------------------------------------------------------------------
            while (hitInfo.collider != null)
            {
                // Drawing line
                endPosition = hitInfo.point;
                _lineRenderer.SetPositions(new Vector3[] { transform.position, endPosition });

                // finding the direction
                hitInfo = Physics2D.Raycast(transform.position, transform.right, lineLength, layerDetection);
                direction = (hitInfo.point - (Vector2)transform.position).normalized;
                pointDetacted = hitInfo.point;

                yield return null;
            }

            // -------- Didn't finde collision ----------------------------------------------------------------------------

            direction = Vector2.zero;
            pointDetacted = Vector2.zero;

            while (hitInfo.collider == null)
            {
                endPosition = transform.position + (transform.right * lineLength);
                _lineRenderer.SetPositions(new Vector3[] { transform.position, endPosition });
                hitInfo = Physics2D.Raycast(transform.position, transform.right, lineLength, layerDetection);

                yield return null;
            }

            // -------- Restart -------------------------------------------------------------------------------------------

            myCoroutine = StartCoroutine(CheckForPoint());
        }
    }

}
