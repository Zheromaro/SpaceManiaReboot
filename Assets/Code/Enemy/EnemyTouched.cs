using UnityEngine;
using SpaceGame.Stats;
using SpaceGame.Player;

namespace SpaceGame.Enemy
{
    public class EnemyTouched : MonoBehaviour
    {
        [SerializeField] private int damageForMe;
        private EnemyHealth health;

        void Start()
        {
            health = GetComponent<EnemyHealth>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") == false) return;

            if (health != null)
            {
                health.GetHit(damageForMe, collision.gameObject);
            }

            //StatsManager.statsManager._PlayerHealth.DmgUnit( gameObject);

            this.enabled = true;
        }
    }
}
