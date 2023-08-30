using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private EnemySpawnerAttributes _enemySpawnerAttributes;

    private float _spawnTime;

    private ObjectPoolWrapper _enemyPool;

    private void Awake()
    {
        _enemyPool = new ObjectPoolWrapper(_enemySpawnerAttributes.EnemyPrefab, _enemySpawnerAttributes.EnemyPoolSize);
        _spawnTime = Random.Range(2, _enemySpawnerAttributes.MaximumSpawnTime);
    }

    private void Update()   
    {
        _spawnTime -= Time.deltaTime;

        if (_spawnTime <= 0)
        {
            GameObject enemy = _enemyPool.GetFromPool();
            enemy.transform.position = transform.position;

            SetEnemyAttributes(enemy);
            SetSpawnTime();
        }
    }

    private void SetEnemyAttributes(GameObject enemy)
    {
        EnemySpawn enemySpawn = enemy.GetComponent<EnemySpawn>();

        float currentMaximumEnemyAttributeModifier = _enemySpawnerAttributes.MinimumEnemyAttributeModifier + (_enemySpawnerAttributes.EnemyDifficultyIncreasePerSecond * Time.timeSinceLevelLoad);
        float enemyAttributeModifier = Random.Range(_enemySpawnerAttributes.MinimumEnemyAttributeModifier, currentMaximumEnemyAttributeModifier);
        enemyAttributeModifier = Mathf.Clamp(enemyAttributeModifier, _enemySpawnerAttributes.MinimumEnemyAttributeModifier, _enemySpawnerAttributes.MaximumEnemyAttributeModifier);

        enemySpawn.OnSpawn(enemyAttributeModifier);        
    }

    private void SetSpawnTime()
    {
        float currentMinimumSpawnTime = _enemySpawnerAttributes.MaximumSpawnTime - (_enemySpawnerAttributes.SpawnRateIncreasePerSecond * Time.timeSinceLevelLoad);
        
        _spawnTime = Random.Range(currentMinimumSpawnTime, _enemySpawnerAttributes.MaximumSpawnTime);

        _spawnTime = Mathf.Clamp(_spawnTime, _enemySpawnerAttributes.MinimumSpawnTime, _enemySpawnerAttributes.MaximumSpawnTime);
    }
}
