using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolWrapper
{
    private GameObject _objectToPool;

    private IObjectPool<GameObject> _pool;

    public ObjectPoolWrapper(GameObject objectToPool, int poolSize)
    {
        _objectToPool = objectToPool;

        _pool = new ObjectPool<GameObject>(CreateObject, OnGetFromPool, OnReleaseFromPool, OnPoolDestroy, true, poolSize, poolSize);
    }

    public GameObject GetFromPool()
    {
        GameObject obj = _pool.Get();

        ReturnToObjectPoolController returnToObjectPool = obj.GetComponent<ReturnToObjectPoolController>();

        if (returnToObjectPool != null)
        {
            returnToObjectPool.ObjectPool = _pool;
        }

        return obj;
    }

    private GameObject CreateObject()
    {
        return GameObject.Instantiate(_objectToPool);
    }

    private void OnGetFromPool(GameObject obj)
    {
        obj.SetActive(true);
    }

    private void OnReleaseFromPool(GameObject obj)
    {
        obj.SetActive(false);
    }

    private void OnPoolDestroy(GameObject obj)
    {
        GameObject.Destroy(obj);
    }
}
