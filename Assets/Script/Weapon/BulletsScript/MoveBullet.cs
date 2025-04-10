
using System.Collections;
using UnityEngine;

public class MoveBullet : IBulletComponent
{
    private CoroutinePerformer _coroutinePerformer;
    //private BulletConfig _config;

    private Rigidbody _bulletRigidbody;
    private Transform _bulletTransform;

    private float _bulletSpeed;
    private float _distanceFlying;

    private Vector3 _startPoint;

    public MoveBullet(BulletConfig config, CoroutinePerformer routinePerformer)
    {
        //_config = config;

        _coroutinePerformer = routinePerformer;

        _bulletSpeed = config.BaseBulletSpeed;
    }

    public void Initialize(Bullet bullet, Vector3 startPoint, float distanceFlying)
    {
        _startPoint = startPoint;
        _distanceFlying = distanceFlying;

        _bulletRigidbody = bullet.GetRigidbodyBullet();
        _bulletTransform = bullet.GetTransformBullet();
        
        BulletMove();
    }
    
    private void BulletMove()
    {
        _coroutinePerformer.StartCoroutine(CheckDistanceFlyingJob());

        _bulletRigidbody.velocity = _bulletTransform.forward * _bulletSpeed;
    }

    private IEnumerator CheckDistanceFlyingJob()
    {
        while (Vector3.Distance(_startPoint, _bulletTransform.position) < _distanceFlying)
        {
            yield return null;
        }

        // Вызов метода отвечающий за возврат пули в пул.
    }
}
