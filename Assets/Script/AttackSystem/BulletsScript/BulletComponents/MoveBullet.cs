using System.Collections;
using UnityEngine;

public class MoveBullet
{
    private CoroutinePerformer _coroutinePerformer;
    //private BulletConfig _config;

    private Rigidbody _bulletRigidbody;
    private Transform _bulletTransform;
    private Bullet _bullet;

    private float _bulletSpeed;
    private float _distanceFlying;

    private Vector3 _startPoint;

    public MoveBullet(CoroutinePerformer routinePerformer)
    {
        _coroutinePerformer = routinePerformer;
    }

    public void Initialize(BulletConfig config, Bullet bullet, Vector3 startPoint, float distanceFlying)
    {
        _bulletSpeed = config.BaseBulletSpeed;

        _startPoint = startPoint;
        _distanceFlying = distanceFlying;

        _bullet = bullet;
        _bulletRigidbody = bullet.GetRigidbodyBullet();
        Debug.Log("BulletRigidbody = " + _bulletRigidbody);

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

        _bullet?.ReturnToPool();
    }
}
