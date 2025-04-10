using UnityEngine;
using Zenject;

public abstract class Bullet : MonoBehaviour
{
    private BulletConfig _bulletConfig;

    private AttackBullet _attackBullet;
    private MoveBullet _moveBullet;

    private Rigidbody _rigidbody;

    [Inject]
    private void Consturct(AttackBullet attackBullet, MoveBullet moveBullet)
    {
        _attackBullet = attackBullet;
        _moveBullet = moveBullet;

        _rigidbody = GetComponent<Rigidbody>();
        _moveBullet = GetComponent<MoveBullet>();
    }

    public Rigidbody GetRigidbodyBullet() => _rigidbody;
    
    public Transform GetTransformBullet() => transform;

    public void InitializeBullet(Vector3 startPoint, Quaternion rotateDirection, float distanceFlying)
    {
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

        _moveBullet.Initialize(this, startPoint, distanceFlying);
    }

    private void OnTriggerEnter(Collider collider)
    {
        _attackBullet.OnTriggerEnter(collider);
    }

}
