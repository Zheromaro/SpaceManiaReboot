using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceGame.Enemy
{
    public class EnemyHealth: MonoBehaviour
    {
        [SerializeField] private int currentHealth, maxHealth;
        [SerializeField] private float delayOfDeath;
        [SerializeField] private bool isDead;

        public UnityEvent<GameObject> OnHitWithRefrence, OnDeathWithRefrence;

        public void Start()
        {
            currentHealth = maxHealth;
            isDead = false;
        }

        public void GetHit(int amount, GameObject sender)
        {
            if (isDead)
                return;

            currentHealth -= amount;
             
            if(currentHealth > 0)
            {
                OnHitWithRefrence?.Invoke(sender);
            }
            else
            {
                OnDeathWithRefrence?.Invoke(sender);
                StartCoroutine(Reset());
            }
        }

        private IEnumerator Reset()
        {
            yield return new WaitForSeconds(delayOfDeath);
            isDead = true;
            Destroy(gameObject);
        }
    }
}
