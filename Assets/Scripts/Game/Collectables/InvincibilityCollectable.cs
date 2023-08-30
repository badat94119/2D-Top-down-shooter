using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityCollectable : MonoBehaviour, ICollectableBehaviour
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
        _playerInvincibility = FindObjectOfType<PlayerInvincibility>();
    }

    public void OnCollected()
    {
        _playerInvincibility.StartInvincibility(_invincibilityLength, _invincibilityFlashColor, _numberOfFlashes);
    }
}
