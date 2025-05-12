using System.Collections;
using UnityEngine;

public abstract class TurretWeapon
{
    protected const float DelayBeforeRotating = 0.6f;
    protected const float MinValueByXYZ = 0;
    protected const float MinAngleBetweenObjects = 0.1f;

    protected AttackType _attackType;
    protected BulletType _bulletType;
    protected float _baseDamage;
    protected float _baseReloadingTime;
    protected float _baseDistanceFlyingBullet;
    protected float _baseRotationSpeed;
    protected float _minAttackRange;
    protected LayerMask _targetLayer;

    protected int _currentSpawnPointBulletIndex = 0;
    protected float _sqrMinAttackRange;
    protected float _currentAttackSpeed;

    protected bool _isWork = false;
    protected bool _isRotatedToTarget = false;

    protected ITurret _currentTurret;

    protected IEnemy _currentTarget;

    protected IFactory<Bullet, BulletConfig, BulletType> _bulletFactory;
    protected TurretSearchTargetSystem _searchTargetSystem;

    protected SpawnPointBullet[] _spawnPointsBullet;
    protected SpawnPointBullet _currentSpawnPointBullet;

    protected GameObject _bodyTurret;

    protected CoroutinePerformer _coroutinePerformer;
    protected Coroutine _attackCoroutine;
    protected Coroutine _rotateToTargetCoroutine;
    protected Coroutine _returnTurretToDefaultRotationCoroutine;

    protected Quaternion _defaultTurretRotation;

    public TurretWeapon(TurretSearchTargetSystem turretSearchTargetSystem, 
        IFactory<Bullet, BulletConfig, BulletType> bulletFactory, 
        CoroutinePerformer coroutinePerformer)
    {
        _coroutinePerformer = coroutinePerformer;

        _searchTargetSystem = turretSearchTargetSystem;

        _bulletFactory = bulletFactory;
    }

    protected abstract void SpawnBullet();
    protected abstract IEnumerator AttackJob();
    protected abstract IEnumerator RotateToTargetJob();

    public void SetTarget(IEnemy target)
    {
        _isWork = true;
        _currentTarget = target;

        if (_rotateToTargetCoroutine != null)
        {
            _coroutinePerformer.StopCoroutine(_rotateToTargetCoroutine);
            _rotateToTargetCoroutine = null;
        }

        _rotateToTargetCoroutine = _coroutinePerformer.StartCoroutine(RotateToTargetJob());

        if (_attackCoroutine != null)
        {
            _coroutinePerformer.StopCoroutine(_attackCoroutine);
            _attackCoroutine = null;
        }

        _attackCoroutine = _coroutinePerformer.StartCoroutine(AttackJob());
    }

    public void ResetTarget(IEnemy target)
    {
        _isWork = false;
        _currentTarget = target;

        if (_returnTurretToDefaultRotationCoroutine != null)
        {
            _coroutinePerformer.StopCoroutine(_returnTurretToDefaultRotationCoroutine);
            _returnTurretToDefaultRotationCoroutine = null;
        }

        _returnTurretToDefaultRotationCoroutine = _coroutinePerformer.StartCoroutine(ReturnTurretToDefaultRotation());
    }

    protected IEnumerator ReturnTurretToDefaultRotation()
    {
        yield return new WaitForSeconds(DelayBeforeRotating);

        float divider = 3f;
        float angleBetweenTargetAndTurret = Quaternion.Angle(_bodyTurret.transform.rotation, _defaultTurretRotation);

        while (angleBetweenTargetAndTurret > MinAngleBetweenObjects && _currentTarget == null)
        {
            float rotationSpeed = (Time.deltaTime * _baseRotationSpeed) / divider;

            Quaternion rotate = Quaternion.Slerp(_bodyTurret.transform.rotation, _defaultTurretRotation, rotationSpeed);

            _bodyTurret.transform.rotation = rotate;

            yield return null;
        }
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

    protected Vector3 GetPredictionTargetPosition(IEnemy target)
    {
        if (target == null)
            return Vector3.zero;

        BulletConfig bulletConfig = _bulletFactory.GetObjectConfig(_bulletType);
        float bulletSpeed = bulletConfig.BaseBulletSpeed;

        Vector3 targetPos = target.Transform.position;
        Vector3 targetVelocity = target.NavMeshAgent.velocity;

        float distanceToTarget = Vector3.Distance(_bodyTurret.transform.position, targetPos);
        float timeToTarget = distanceToTarget / bulletSpeed;

        return targetPos + targetVelocity * timeToTarget;
    }

    protected void SetAttackProperties(TurretConfig config)
    {
        _attackType = config.AttackCharacteristics.AttackType;
        _bulletType = config.AttackCharacteristics.BulletType;

        _baseDamage = config.AttackCharacteristics.BaseDamage;
        _baseReloadingTime = config.AttackCharacteristics.BaseDelayBeforeFiring;
        _baseDistanceFlyingBullet = config.AttackCharacteristics.BaseAttackRange;
        _baseRotationSpeed = config.AttackCharacteristics.BaseRotationSpeed;
        _minAttackRange = config.AttackCharacteristics.MinimumAttackRange;

        _sqrMinAttackRange = _minAttackRange * _minAttackRange;

        _targetLayer = config.AttackCharacteristics.TargetLayer;

        _currentAttackSpeed = _baseReloadingTime;
    }

    protected void StopCoroutine(Coroutine routine)
    {
        if (routine != null)
        {
            _coroutinePerformer.StopCoroutine(routine);
            routine = null;
        }
    }


}
