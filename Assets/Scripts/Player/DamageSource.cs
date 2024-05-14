using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{

    [SerializeField] private int damageAmount = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<EnemyHealth>())
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(damageAmount);
        }
    }
}
