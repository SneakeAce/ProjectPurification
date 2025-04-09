using System.Collections;
using UnityEngine;

public class Rifle : Weapon
{
    protected override IEnumerator PrepareWeaponToShootingJob()
    {
        throw new System.NotImplementedException();
    }

    protected override void Shooting()
    {
        Quaternion rotate = Quaternion.Euler(0, _character.transform.eulerAngles.y, 0);

        _currentMagazineCapacity = _currentMagazineCapacity - ReleasedBulletsOfSingleShootingMode;

        _delayBeforeFiring = _startDelayBeforeFiring;

        Bullet bullet = _bulletPool.GetPoolObject();

        if (bullet == null)
            return;

        bullet.InitializeBullet(_spawnPoint.transform.position, rotate, WeaponConfig.WeaponStatsConfig.BaseRangeShooting, 
            WeaponConfig.WeaponStatsConfig.BaseDamage, ReturnBulletToPool);

        CurrentValueChange();
    }

    private void ReturnBulletToPool(Bullet bullet)
    {
        _bulletPool.ReturnPoolObject(bullet);
    }
}
