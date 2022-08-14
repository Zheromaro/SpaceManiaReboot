using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 40;
    [SerializeField] private Rigidbody2D rb;

    void FixedUpdate()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        GameObject effect = ObjectPool.instance.GetPooledEffect();
         if (effect != null)
        {
            effect.transform.position = transform.position;
            effect.SetActive(true);
        }

        gameObject.SetActive(false);
    }
}
