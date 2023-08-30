using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Camera _camera;
    private ReturnToObjectPoolController _returnToObjectPoolController;
    private WeaponAttributes _weaponAttributes;

    public void SetWeaponAttributes(WeaponAttributes weaponAttributes)
    {
        _weaponAttributes = weaponAttributes;
    }

    private void Awake()
    {
        _camera = Camera.main;
        _returnToObjectPoolController = GetComponent<ReturnToObjectPoolController>();    
    }

    private void Update()
    {
        ReturnToPoolWhenOffScreen();
    }

    private void ReturnToPoolWhenOffScreen()
    {
        Vector2 screenPoint = _camera.WorldToScreenPoint(transform.position);

        if (screenPoint.x < 0 ||
            screenPoint.x > _camera.pixelWidth ||
            screenPoint.y < 0 ||
            screenPoint.y > _camera.pixelHeight)
        {
            _returnToObjectPoolController.ReturnToObjectPool();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyMovement>())
        {
            HealthController healthController = collision.GetComponent<HealthController>();
            healthController.TakeDamage(_weaponAttributes.BulletDamage);

            _returnToObjectPoolController.ReturnToObjectPool();
        }        
    }
}
