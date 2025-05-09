using UnityEngine;

public abstract class Barrier : MonoBehaviour, IBarrier, IPoolable
{
    protected BarrierConfig _config;
    protected BarrierHealth _barrierHealth;
    protected CoroutinePerformer _coroutinePerformer;

    private ObjectPool<Barrier> _pool;

    public Transform Transform => transform;
    public Collider Collider => GetComponent<Collider>();
    public Animator Animator => throw new System.NotImplementedException();
    public Rigidbody Rigidbody => throw new System.NotImplementedException();
    public IEntityHealth EntityHealth => _barrierHealth;

    public abstract void Initialization();

    public void SetPool<T>(ObjectPool<T> pool) where T : MonoBehaviour
    {
        _pool = pool as ObjectPool<Barrier>;
    }

    public void ReturnToPool(IEntity entity)
    {
        _pool?.ReturnPoolObject(this);

        _pool = null;
    }

    public void SetComponents(BarrierConfig config)
    {
        _config = config;

        Initialization();
    }

}
