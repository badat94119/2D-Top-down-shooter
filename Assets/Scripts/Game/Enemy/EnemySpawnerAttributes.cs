using UnityEngine;
using UnityEngine.Pool;

[CreateAssetMenu(fileName = "Enemy Spawner Attributes", menuName = "ScriptableObjects/Enemy Spawner Attributes", order = 1)]
public class EnemySpawnerAttributes : ScriptableObject
{
    [field: SerializeField]
    public GameObject EnemyPrefab { get; private set; }

    [field: SerializeField]
    public int EnemyPoolSize { get; private set; }

    [field: SerializeField]
    public float MaximumSpawnTime { get; private set; }

    [field: SerializeField]
    public float MinimumSpawnTime { get; private set; }

    [field: SerializeField]
    public float SpawnRateIncreasePerSecond { get; private set; }

    [field: SerializeField]
    public float MinimumEnemyAttributeModifier { get; private set; }

    [field: SerializeField]
    public float MaximumEnemyAttributeModifier { get; private set; }

    [field: SerializeField]
    public float EnemyDifficultyIncreasePerSecond { get; private set; }
}