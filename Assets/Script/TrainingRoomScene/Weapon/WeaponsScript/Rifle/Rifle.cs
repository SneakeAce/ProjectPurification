using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Rifle : Weapon
{
    [SerializeField] private LayerMask _includeLayer;
    [SerializeField] private GameObject _spawnPoint;

    public override void Initialize(Character character)
    {
        _bulletPool = new ObjectPool<Bullet>(_bulletPrefab, _maxPoolSize, _poolHolder.transform);
        base.Initialize(character);
    }

    protected override IEnumerator PrepareWeaponToShootingJob()
    {
        throw new System.NotImplementedException();
    }

    protected override IEnumerator ReloadingJob(float timeReload)
    {
        while (timeReload > 0)
        {
            timeReload -= Time.deltaTime;
            yield return null;
        }

        _currentMagazineCapacity = _maxMagazineCapacity;
        CurrentValueChange();

        _isReloading = false;

        StopCoroutine(_reloadingWeaponCoroutine);
        _reloadingWeaponCoroutine = null;
    }

    protected override void Shooting()
    {
        Quaternion rotate = Quaternion.Euler(0, _character.transform.eulerAngles.y, 0);

        _currentMagazineCapacity = _currentMagazineCapacity - _currentReleasedBulletAtTime;

        _delayBeforeFiring = _startDelayBeforeFiring;

        Bullet bullet = _bulletPool.GetPoolObject();

        if (bullet == null)
            return;

        bullet.InitializeBullet(_spawnPoint.transform.position, rotate, WeaponConfig.WeaponStatsConfig.RangeShooting, 
            WeaponConfig.WeaponStatsConfig.Damage, ReturnBulletToPool);

        CurrentValueChange();
    }

    private void ReturnBulletToPool(Bullet bullet)
    {
        _bulletPool.ReturnPoolObject(bullet);
    }
}
