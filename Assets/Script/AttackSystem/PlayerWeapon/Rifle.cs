using System.Collections;
using UnityEngine;

public class Rifle : Weapon
{
    protected override IEnumerator PrepareWeaponToShootingJob()
    {
        yield return null;
    }

    protected override void Shooting()
    {
        Quaternion rotate = Quaternion.Euler(MinRotationByXYZ, _character.transform.eulerAngles.y, MinRotationByXYZ);

        _currentMagazineCapacity = _currentMagazineCapacity - ReleasedBulletsOfSingleShootingMode;
        _currentMagazineCapacity = Mathf.Clamp(_currentMagazineCapacity, MinMagazineCapacity, MaxMagazineCapacity);

        SpawnBullet(rotate);

        CurrentMagazineValueChange();
    }

    protected override void SpawnBullet(Quaternion rotate)
    {
        Bullet bullet = GetBullet(rotate);

        if (bullet == null)
            return;

        bullet.InitializeBullet(_spawnPointBullet.transform.position, rotate, _baseShootingRange);
    }
}
