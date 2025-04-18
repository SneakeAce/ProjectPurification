using System.Collections;
using UnityEngine;

public abstract class TurretAttack
{
    //Добавить GameObject'ы пушек и тела турели для реализации поворота в сторону врага.

    protected const float MinDelayForCalculateRotate = 0.1f;

    protected AttackType _attackType;
    protected BulletType _bulletType;
    protected float _baseDamage;
    protected float _baseReloadingTime;
    protected float _baseDistanceFlyingBullet;
    protected LayerMask _targetLayer;

    protected bool _isCanAttack = false;

    protected IEnemy _currentTarget;

    protected IFactory<Bullet, BulletType> _bulletFactory;
    protected SpawnPointBullet _spawnPointBullet;

    protected CoroutinePerformer _coroutinePerformer;

    protected Coroutine _attackCoroutine;
    protected Coroutine _rotateToTargetCoroutine;

    public TurretAttack(IFactory<Bullet, BulletType> bulletFactory, CoroutinePerformer performer)
    {
        _bulletFactory = bulletFactory;
        _coroutinePerformer = performer;
    }

    protected abstract void SpawnBullet();

    protected abstract IEnumerator AttackJob();
    protected abstract IEnumerator RotateToTarget();

    public void Initialize(TurretConfig config)
    {
        SetAttackProperties(config);
    }

    public void SetTarget(IEnemy target)
    {
        _isCanAttack = false;
        _currentTarget = target;

        if (_rotateToTargetCoroutine != null)
        {
            _coroutinePerformer.StopCoroutine(_rotateToTargetCoroutine);
            _rotateToTargetCoroutine = null;
        }

        _rotateToTargetCoroutine = _coroutinePerformer.StartCoroutine(RotateToTarget());
    }

    private void SetAttackProperties(TurretConfig config)
    {
        _attackType = config.AttackCharacteristics.AttackType;
        _bulletType = config.AttackCharacteristics.BulletType;

        _baseDamage = config.AttackCharacteristics.BaseDamage;
        _baseReloadingTime = config.AttackCharacteristics.BaseDelayBeforeFiring;
        _baseDistanceFlyingBullet = config.AttackCharacteristics.BaseAttackRange;

        _targetLayer = config.AttackCharacteristics.TargetLayer;
    }

}
