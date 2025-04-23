using System.Collections;
using UnityEngine;

public class AutomaticTurretWeapon : TurretWeapon
{
    private const float MultiplierAttackSpeed = 0.6f;

    private float _currentAttackSpeed;

    public override void Initialize(ITurret currentTurret, TurretSearchTargetSystem turretSearchTargetSystem, 
        TurretConfig config, IFactory<Bullet, BulletType> bulletFactory)
    {
        base.Initialize(currentTurret, turretSearchTargetSystem, config, bulletFactory);

        _currentAttackSpeed = _baseReloadingTime * MultiplierAttackSpeed;
    }

    protected override IEnumerator RotateToTarget()
    {
        while (_isWork == true)
        {
            if (_currentTarget == null)
                break;
            
            Vector3 predictionTargetPosition = GetPredictionTargetPosition(_currentTarget, PredictionFactor);
            Vector3 direction = predictionTargetPosition - _bodyTurret.transform.position;

            Quaternion lookRotation = Quaternion.LookRotation(direction);

            float angleBetweenTargetAndTurret = Quaternion.Angle(_bodyTurret.transform.rotation, lookRotation);

            if (angleBetweenTargetAndTurret > MinAngleBetweenObjects) // убрать магическое число!
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
            float sqrDistanceToTarget = (_currentTarget.Transform.position - _bodyTurret.transform.position).sqrMagnitude;

            if (sqrDistanceToTarget < _sqrMinAttackRange)
            {
                yield return null;
                continue;
            }

            Debug.Log("TurretWeapon / target == " + _currentTarget);

            yield return new WaitForSeconds(_currentAttackSpeed);

            SpawnBullet();
        }
    }

    protected override void SpawnBullet()
    {
        SpawnPointBullet spawnPoint = GetSpawnPointBullet();

        Quaternion rotationBullet = Quaternion.Euler(
            MinValueByXYZ, 
            spawnPoint.transform.eulerAngles.y, 
            MinValueByXYZ);

        Bullet bullet = _bulletFactory.Create(spawnPoint.transform.position, _bulletType, rotationBullet);

        if (bullet == null)
            return;

        bullet.InitializeBullet(spawnPoint.transform.position, rotationBullet, _baseDistanceFlyingBullet);
    }

    private void OnDestroy()
    {
        if (_rotateToTargetCoroutine != null)
        {
            StopCoroutine(_rotateToTargetCoroutine);
            _rotateToTargetCoroutine = null;
        }

        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
            _attackCoroutine = null;
        }
    }

}
