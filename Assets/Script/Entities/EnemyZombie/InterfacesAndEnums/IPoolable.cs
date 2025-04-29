using UnityEngine;

public interface IPoolable
{
    void SetPool<T>(ObjectPool<T> pool) where T : MonoBehaviour;
}
