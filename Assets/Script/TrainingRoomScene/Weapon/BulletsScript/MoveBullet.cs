using System;
using System.Collections;
using UnityEngine;

public class MoveBullet : MonoBehaviour
{
    private float _bulletSpeed;

    private float _distanceFlying;
    private Vector3 _startPoint;

    private Bullet _bullet;

    private Coroutine _checkDistanceFlyingCoroutine;

    private Action<Bullet> _returnToPool;

    public void Initialize(Bullet bullet, Vector3 startPoint, float distanceFlying, float bulletSpeed, Action<Bullet> returnToPool)
    {
        _returnToPool = returnToPool;

        _distanceFlying = distanceFlying;
        _bulletSpeed = bulletSpeed;

        _startPoint = startPoint; 

        _bullet = bullet;

        BulletMove();

        _checkDistanceFlyingCoroutine = StartCoroutine(CheckDistanceFlyingJob());
    }
    
    private void BulletMove()
    {
        _bullet.Rigidbody.velocity = transform.forward * _bulletSpeed;
    }

    private void DeactivateBullet()
    {
        _returnToPool?.Invoke(_bullet);
    }

    private IEnumerator CheckDistanceFlyingJob()
    {
        while (Vector3.Distance(_startPoint, _bullet.transform.position) < _distanceFlying)
        {
            yield return null;
        }

        DeactivateBullet();

        StopCoroutine(_checkDistanceFlyingCoroutine);
        _checkDistanceFlyingCoroutine = null;
    }
}
