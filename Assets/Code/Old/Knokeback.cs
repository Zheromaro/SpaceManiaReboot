using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceGame.NotImportant
{
    public class Knokeback : MonoBehaviour
    {
        [SerializeField] private float strength = 16, delay = 0.15f;
        private Rigidbody2D rb;
        public UnityEvent OnBegin, OnDone;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public void PlayFeedback(GameObject sender)
        {
            StopAllCoroutines();
            OnBegin?.Invoke();
            Vector2 direction = (transform.position - sender.transform.position).normalized;
            rb.AddForce(direction * strength, ForceMode2D.Impulse);
            StartCoroutine(Reset());
        }

        private IEnumerator Reset()
        {
            yield return new WaitForSeconds(delay);
            rb.velocity = Vector2.zero;
            OnDone?.Invoke();
        }
    }
}
