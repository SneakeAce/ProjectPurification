using System.Collections;
using UnityEngine;
using Zenject;

public abstract class TurretWeapon : MonoBehaviour
{
    //Добавить GameObject'ы пушек и тела турели для реализации поворота в сторону врага.

    protected const float MinDelayForCalculateRotate = 0.1f;

    protected AttackType _attackType;
    protected BulletType _bulletType;
    protected float _baseDamage;
    protected float _baseReloadingTime;
    protected float _baseDistanceFlyingBullet;
    protected LayerMask _targetLayer;

    protected int _currentSpawnPointBulletIndex = 0;

    protected bool _isCanAttack = false;

    protected IEnemy _currentTarget;

    protected IFactory<Bullet, BulletType> _bulletFactory;

    protected SpawnPointBullet[] _spawnPointsBullet;
    protected SpawnPointBullet _curretSpawnPointBullet;

    protected GameObject _bodyTurret;

    protected Coroutine _attackCoroutine;
    protected Coroutine _rotateToTargetCoroutine;

    protected abstract void SpawnBullet();

    protected abstract IEnumerator AttackJob();
    protected abstract IEnumerator RotateToTarget();

    public virtual void Initialize(TurretConfig config, IFactory<Bullet, BulletType> bulletFactory)
    {
        _bulletFactory = bulletFactory;

        _bodyTurret = this.gameObject;

        _spawnPointsBullet = GetComponentsInChildren<SpawnPointBullet>();

        SetAttackProperties(config);
    }

    public void SetTarget(IEnemy target)
    {
        _isCanAttack = false;
        _currentTarget = target;

        if (_rotateToTargetCoroutine != null)
        {
            StopCoroutine(_rotateToTargetCoroutine);
            _rotateToTargetCoroutine = null;
        }

        _rotateToTargetCoroutine = StartCoroutine(RotateToTarget());
    }

    protected SpawnPointBullet GetSpawnPointBullet()
    {
        if (_currentSpawnPointBulletIndex > 1)
            _currentSpawnPointBulletIndex = 0;

        SpawnPointBullet spawnPoint = _spawnPointsBullet[_currentSpawnPointBulletIndex];

        if (spawnPoint == null)
            return null;

        _currentSpawnPointBulletIndex++;

        return spawnPoint;
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
