using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    private EnemyMovement _enemyMovement;
    private CircleCollider2D _collider;
    private EnemyAttributes _enemyAttributes;
    private HealthController _healthController;
    private SpriteFlash _spriteFlash;

    private void Awake()
    {
        _enemyMovement = GetComponent<EnemyMovement>();
        _collider = GetComponent<CircleCollider2D>();
        _enemyAttributes = GetComponent<EnemyAttributes>();
        _healthController = GetComponent<HealthController>();
        _spriteFlash = GetComponentInChildren<SpriteFlash>();
    }

    public void OnSpawn(float attributeModifier)
    {
        _enemyMovement.enabled = true;
        _collider.enabled = true;
        _enemyAttributes.SetAttributeModifier(attributeModifier);
        _healthController.SetHealth(_enemyAttributes.Health);
        _spriteFlash.SetOriginalColor(new Color(1f / attributeModifier, 1f / attributeModifier, 1f));
    }
}
