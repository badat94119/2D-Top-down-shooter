using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _collectablePrefabs;

    private List<ObjectPoolWrapper> _collectablePools;

    private void Awake()
    {
        _collectablePools = new List<ObjectPoolWrapper>(_collectablePrefabs.Count);

        foreach (var collectablePrefab in _collectablePrefabs)
        {
            _collectablePools.Add(new ObjectPoolWrapper(collectablePrefab, 20));
        }
    }

    public void SpawnCollectable(Vector2 position)
    {
        int index = Random.Range(0, _collectablePools.Count);

        GameObject collectable = _collectablePools[index].GetFromPool();
        collectable.transform.position = position;
    }
}
