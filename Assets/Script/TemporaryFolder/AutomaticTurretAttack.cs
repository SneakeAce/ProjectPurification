using System.Collections;
using UnityEngine;

public class AutomaticTurretAttack : TurretAttack
{
    private const float MultiplierAttackSpeed = 0.6f;

    private float _currentAttackSpeed;

    public AutomaticTurretAttack(IFactory<Bullet, BulletType> bulletFactory, 
        CoroutinePerformer performer) : base(bulletFactory, performer)
    {

        _currentAttackSpeed = _baseReloadingTime * MultiplierAttackSpeed;
    }


    protected override IEnumerator RotateToTarget()
    {
        while (_isCanAttack == false)
        {
            if (_currentTarget == null)
                break;

            // ��� ��� �������� �������� ������ ������.
            // ��� ��� �������� �������� ���������� ����� ����� � ������� ������.

            _isCanAttack = true;
            yield return new WaitForSeconds(MinDelayForCalculateRotate);
        }

        if (_attackCoroutine != null)
        {
            _coroutinePerformer.StopCoroutine(_attackCoroutine);
            _attackCoroutine = null;
        }

        _attackCoroutine = _coroutinePerformer.StartCoroutine(AttackJob());
    }

    protected override IEnumerator AttackJob()
    {
        while (_currentTarget != null)
        {
            yield return new WaitForSeconds(_currentAttackSpeed);

            // ������ �������� �� ������� ����������, ���� ��� ������, �� ����� �������� ����� � ��������� ������ � ������� �����.

            SpawnBullet();
        }
    }

    protected override void SpawnBullet()
    {
        Bullet bullet = _bulletFactory.Create(_spawnPointBullet.transform.position, _bulletType, Quaternion.identity);

        if (bullet == null)
            return;

        bullet.InitializeBullet(_spawnPointBullet.transform.position, Quaternion.identity, _baseDistanceFlyingBullet);
    }
}
