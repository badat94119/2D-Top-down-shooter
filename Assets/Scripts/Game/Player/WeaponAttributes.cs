using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Attributes", menuName = "ScriptableObjects/Weapon Attributes", order = 1)]
public class WeaponAttributes : ScriptableObject
{
    [field: SerializeField]
    public GameObject BulletPrefab { get; private set; }

    [field: SerializeField]
    public float TimeBetweenShots { get; private set; }

    [field: SerializeField]
    public float BulletSpeed { get; private set; }

    [field: SerializeField]
    public float BulletDamage { get; private set; }

    [field: SerializeField]
    public AudioClip BulletAudio { get; private set; }
}
