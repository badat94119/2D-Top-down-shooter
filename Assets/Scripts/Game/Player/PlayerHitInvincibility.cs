using UnityEngine;

public class PlayerHitInvincibility : MonoBehaviour
{
    [SerializeField]
    private float _invincibilityLength;

    [SerializeField]
    private Color _invincibilityFlashColor;

    [SerializeField]
    private int _numberOfFlashes;

    private PlayerInvincibility _playerInvincibility;

    private void Awake()
    {
        _playerInvincibility = GetComponentInChildren<PlayerInvincibility>();
    }

    public void StartInvincibility()
    {
        _playerInvincibility.StartInvincibility(_invincibilityLength, _invincibilityFlashColor, _numberOfFlashes);
    }
}
