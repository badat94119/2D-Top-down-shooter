using UnityEngine;

public class WeaponCollectable : MonoBehaviour, ICollectableBehaviour
{
    [SerializeField]
    private WeaponAttributes _weaponAttributes;

    private PlayerShoot _playerShoot;
    private Weapon _weapon;

    private void Awake()
    {
        _weapon = new Weapon(_weaponAttributes);
        _playerShoot = FindObjectOfType<PlayerShoot>();
    }

    public void OnCollected()
    {
        _playerShoot.SetWeapon(_weapon);
    }
}
