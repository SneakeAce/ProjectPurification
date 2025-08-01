
using UnityEngine;
using Zenject;

public abstract class Bullet : MonoBehaviour, IPoolable
{
    private BulletConfig _bulletConfig;

    private AttackBullet _attackBullet;
    private MoveBullet _moveBullet;

    private Rigidbody _rigidbody;
    private CoroutinePerformer _coroutinePerformer;
    private ObjectPool<Bullet> _pool;

    [Inject]
    private void Consturct(CoroutinePerformer coroutinePerformer)
    {
        _coroutinePerformer = coroutinePerformer;

        _rigidbody = GetComponent<Rigidbody>();
    }

    public Rigidbody GetRigidbodyBullet() => _rigidbody;
    
    public Transform GetTransformBullet() => transform;

    public void SetPool<T>(ObjectPool<T> pool) where T : MonoBehaviour
    {
        _pool = pool as ObjectPool<Bullet>;
    }

    public void ReturnToPool()
    {
        _pool?.ReturnPoolObject(this);

        _pool = null;
    }

    public void InitializeBullet(Vector3 startPoint, Quaternion rotateDirection, float distanceFlying)
    {
        _attackBullet = new AttackBullet(this, _bulletConfig);
        _moveBullet = new MoveBullet(_coroutinePerformer);

        StartMoveBullet(startPoint, rotateDirection, distanceFlying);
    }

    public void SetComponents(BulletConfig config)
    {
        _bulletConfig = config;
    }

    private void StartMoveBullet(Vector3 startPoint, Quaternion rotateDirection, float distanceFlying)
    {
        transform.position = startPoint;
        transform.rotation = rotateDirection;

        _moveBullet.Initialize(_bulletConfig, this, startPoint, distanceFlying);
    }

    private void OnTriggerEnter(Collider collider)
    {
        _attackBullet.OnTriggerEnter(collider);
    }

}
