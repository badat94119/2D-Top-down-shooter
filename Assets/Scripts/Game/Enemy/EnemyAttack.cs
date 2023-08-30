using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private EnemyAttributes _enemyAttributes;

    private void Awake()
    {
        _enemyAttributes = GetComponent<EnemyAttributes>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<PlayerMovement>())
        {
            HealthController healthController = collision.collider.GetComponent<HealthController>();

            healthController.TakeDamage(_enemyAttributes.Damage);
        }
    }
}
