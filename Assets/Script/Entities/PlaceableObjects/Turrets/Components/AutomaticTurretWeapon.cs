using System.Collections;
using UnityEngine;

public class AutomaticTurretWeapon : TurretWeapon
{
    public AutomaticTurretWeapon(TurretSearchTargetSystem turretSearchTargetSystem,
        IFactory<Bullet, BulletConfig, BulletType> bulletFactory, 
        CoroutinePerformer coroutinePerformer) : base(turretSearchTargetSystem, bulletFactory, coroutinePerformer)
    {
    }

    public void Initialization(ITurret currentTurret, TurretConfig config, GameObject bodyTurret)
    {
        _currentTurret = currentTurret;
        _currentTurret.EntityHealth.EntityDied += OnDisable;
        _bodyTurret = bodyTurret;

        _spawnPointsBullet = bodyTurret.GetComponentsInChildren<SpawnPointBullet>();
        _defaultTurretRotation = currentTurret.Transform.rotation;

        _searchTargetSystem.Start(_currentTurret, config);

        SetAttackProperties(config);
    }

    protected override IEnumerator RotateToTargetJob()
    {
        while (_isWork == true)
        {
            if (_currentTarget == null)
                break;
            
            Vector3 predictionTargetPosition = GetPredictionTargetPosition(_currentTarget);

            if (predictionTargetPosition == Vector3.zero)
            {
                yield return null;
                continue;
            }

            Vector3 direction = predictionTargetPosition - _bodyTurret.transform.position;

            Quaternion lookRotation = Quaternion.LookRotation(direction);

            float angleBetweenTargetAndTurret = Quaternion.Angle(_bodyTurret.transform.rotation, lookRotation);

            if (angleBetweenTargetAndTurret > MinAngleBetweenObjects)
            {
                float rotationSpeed = Time.deltaTime * _baseRotationSpeed;

                Quaternion rotationToTarget = Quaternion.Slerp(_bodyTurret.transform.rotation, lookRotation, rotationSpeed);

                _bodyTurret.transform.rotation = rotationToTarget;
            }

            yield return null;
        }

    }

    protected override IEnumerator AttackJob()
    {
        while (_currentTarget != null)
        {
            if (_currentTarget == null)
                yield break;

            float sqrDistanceToTarget = (_currentTarget.Transform.position - _bodyTurret.transform.position).sqrMagnitude;

            if (sqrDistanceToTarget < _sqrMinAttackRange)
            {
                yield return null;
                continue;
            }

            yield return new WaitForSeconds(_currentAttackSpeed);

            SpawnBullet();
        }
    }

    protected override void SpawnBullet()
    {
        SpawnPointBullet spawnPoint = GetSpawnPointBullet();

        if (spawnPoint == null || _currentTarget == null)
            return;

        Vector3 predictionTargetPosition = GetPredictionTargetPosition(_currentTarget);

        predictionTargetPosition.y = spawnPoint.transform.position.y;

        Vector3 directionToTarget = (predictionTargetPosition - spawnPoint.transform.position).normalized;

        Quaternion rotationBullet = Quaternion.LookRotation(directionToTarget);

        Bullet bullet = _bulletFactory.Create(spawnPoint.transform.position, _bulletType, rotationBullet);

        if (bullet == null)
            return;

        bullet.InitializeBullet(spawnPoint.transform.position, rotationBullet, _baseDistanceFlyingBullet);
    }

    private void OnDisable(IEntity entity)
    {
        StopCoroutine(_attackCoroutine);
        StopCoroutine(_rotateToTargetCoroutine);

        ResetTarget(null);
    }
}
