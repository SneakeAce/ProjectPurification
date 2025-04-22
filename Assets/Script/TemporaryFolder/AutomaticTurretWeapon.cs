using System.Collections;
using UnityEngine;

public class AutomaticTurretWeapon : TurretWeapon
{
    private const float MultiplierAttackSpeed = 0.6f;

    private float _currentAttackSpeed;

    public override void Initialize(TurretConfig config, IFactory<Bullet, BulletType> bulletFactory)
    {
        base.Initialize(config, bulletFactory);

        _currentAttackSpeed = _baseReloadingTime * MultiplierAttackSpeed;
    }

    protected override IEnumerator RotateToTarget()
    {
        while (_isCanAttack == false)
        {
            if (_currentTarget == null)
                break;

            // Код для поворота раличных частей турели.
            // код для проверки углового расстояния между целью и частями турели.

            _isCanAttack = true;
            yield return new WaitForSeconds(MinDelayForCalculateRotate);
        }

        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
            _attackCoroutine = null;
        }

        _attackCoroutine = StartCoroutine(AttackJob());
    }

    protected override IEnumerator AttackJob()
    {
        while (_currentTarget != null)
        {
            yield return new WaitForSeconds(_currentAttackSpeed);

            // Делать проверку на угловой расстояние, если оно больше, то нужно прервать атаку и повернуть турель в сторону врага.

            SpawnBullet();
        }
    }

    protected override void SpawnBullet()
    {
        SpawnPointBullet spawnPoint = GetSpawnPointBullet();

        Bullet bullet = _bulletFactory.Create(spawnPoint.transform.position, _bulletType, Quaternion.identity);

        if (bullet == null)
            return;

        bullet.InitializeBullet(spawnPoint.transform.position, Quaternion.identity, _baseDistanceFlyingBullet);
    }

    
}
