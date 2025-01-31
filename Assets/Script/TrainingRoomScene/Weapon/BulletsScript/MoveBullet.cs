using System.Collections;
using UnityEngine;

public class MoveBullet : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed;

    private float _distanceFlying;
    private Vector3 _startPoint;

    private Bullet _bullet;

    private Coroutine _checkDistanceCoroutine;

    public void Initialize(Bullet bullet, Vector3 startPoint, float distanceFlying)
    {
        _distanceFlying = distanceFlying;
        _startPoint = startPoint; 

        _bullet = bullet;

        BulletMove();

        _checkDistanceCoroutine = StartCoroutine(CheckDistanceBulletJob());
    }
    
    private void BulletMove()
    {
        //_bullet.Rigidbody.AddForce(transform.forward * _bulletSpeed, ForceMode.Impulse);
        _bullet.Rigidbody.velocity = transform.forward * _bulletSpeed;
    }

    private void DestroyBullet()
    {
        Destroy(this.gameObject);
    }

    private IEnumerator CheckDistanceBulletJob()
    {
        while (Vector3.Distance(_startPoint, transform.position) < _distanceFlying)
        {
            yield return null;
        }

        DestroyBullet();

        StopCoroutine(_checkDistanceCoroutine);
        _checkDistanceCoroutine = null;
    }
}
