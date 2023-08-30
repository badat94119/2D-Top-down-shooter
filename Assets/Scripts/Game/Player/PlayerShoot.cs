using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private WeaponAttributes _defaultWeaponAttributes;

    [SerializeField]
    private Transform _gunOffset;

    private Weapon _defaultWeapon;
    private Weapon _currentWeapon;

    private bool _fireSingle;
    private bool _fireContinuously;

    private AudioSource _audioSource;

    private void Awake()
    {
        _defaultWeapon = new Weapon(_defaultWeaponAttributes);
        _currentWeapon = _defaultWeapon;
        _audioSource = GetComponent<AudioSource>();
    }

    public void ResetToDefaultWeapon()
    {
        _currentWeapon = _defaultWeapon;
    }

    public void SetWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
    }

    private void Update()
    {
#if UNITY_WEBGL
        // For some reason the new Input System isn't working with WebGL, so had to revert to the old Input system
        if ((Input.GetButtonDown("Fire1") || Input.GetAxis("TriggerFire1") == 1) && _fireContinuously == false)
        {
            _fireSingle = true;
        }

        _fireContinuously = Input.GetButton("Fire1") || Input.GetAxis("TriggerFire1") == 1;
#endif
        if (_fireSingle || _fireContinuously)
        {
            if (_currentWeapon.TryFire(_gunOffset.position, transform.up, _audioSource))
            {
                _fireSingle = false;
            }
        }
    }
#if !UNITY_WEBGL
        private void OnFire(InputValue value)
    {
        _fireContinuously = value.isPressed;

        if (value.isPressed)
        {
            _fireSingle = true;
        }
    }
#endif
}
