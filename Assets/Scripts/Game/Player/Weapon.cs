using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    private WeaponAttributes _weaponAttributes;
    private ObjectPoolWrapper _bulletPool;

    private float _lastFireTime;

    public Weapon(WeaponAttributes weaponAttributes)
    {
        _weaponAttributes = weaponAttributes;
        _bulletPool = new ObjectPoolWrapper(weaponAttributes.BulletPrefab, 100);
    }

    public bool TryFire(Vector2 position, Vector2 direction, AudioSource audioSource)
    {
        if (Time.time - _lastFireTime >= _weaponAttributes.TimeBetweenShots)
        {
            Bullet bullet = _bulletPool.GetFromPool().GetComponent<Bullet>();
            bullet.transform.position = position;
            bullet.transform.up = direction;

            Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
            rigidbody.velocity = bullet.transform.up * _weaponAttributes.BulletSpeed;

            audioSource.PlayOneShot(_weaponAttributes.BulletAudio);

            bullet.SetWeaponAttributes(_weaponAttributes);

            _lastFireTime = Time.time;

            return true;
        }

        return false;
    }
}
