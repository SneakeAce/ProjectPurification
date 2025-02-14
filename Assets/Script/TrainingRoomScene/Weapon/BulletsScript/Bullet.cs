using DG.Tweening;
using System;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private MoveBullet _moveBullet;

    protected float _damage;

    protected Action<Bullet> _returnToPool;

    public Rigidbody Rigidbody { get => _rigidbody; }

    public abstract void DamageDeal(EnemyCharacter unit);

    public void InitializeBullet(Vector3 startPoint, Quaternion rotateDirection, float distanceFlying, float bulletDamage, Action<Bullet> returnToPool)
    {
        this.transform.position = startPoint;
        this.transform.rotation = rotateDirection;

        _returnToPool = returnToPool;

        _damage = bulletDamage;

        _moveBullet.Initialize(this, startPoint, distanceFlying, _returnToPool);
    }

    private void OnTriggerEnter(Collider collision)
    {
        //Debug.Log("Collision == " + collision);

        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            EnemyCharacter target = collision.gameObject.GetComponent<EnemyCharacter>();

            Debug.Log("Bullet enter collision / target = " + target);

            DamageDeal(target);

            DeactivateBullet();
        }
    }

    private void DeactivateBullet()
    {
        _returnToPool?.Invoke(this);
    }

}
