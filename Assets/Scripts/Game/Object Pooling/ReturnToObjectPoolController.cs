using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ReturnToObjectPoolController : MonoBehaviour
{
    public IObjectPool<GameObject> ObjectPool { get; set; }

    public void ReturnToObjectPool(float delay)
    {
        Invoke(nameof(ReturnToObjectPool), delay);
    }

    public void ReturnToObjectPool()
    {
        ObjectPool.Release(gameObject);
    }
}
