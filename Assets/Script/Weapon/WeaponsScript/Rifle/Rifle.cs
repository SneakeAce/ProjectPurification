using System.Collections;
using UnityEngine;

public class Rifle : Weapon
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _spawnPoint;
    private GameObject _instanceBulletPrefab;

    private Vector3 _directionToMousePosition;

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
        _isReloading = false;

        Debug.Log("ReloadingJob Done");

        StopCoroutine(_reloadingWeaponCoroutine);
        _reloadingWeaponCoroutine = null;
    }

    protected override void Shooting()
    {
        Quaternion rotate = GetDirection();

        _currentMagazineCapacity = _currentMagazineCapacity - _currentReleasedBulletAtTime;

        _delayBeforeFiring = _startDelayBeforeFiring;

        _instanceBulletPrefab = Instantiate(_bulletPrefab, _spawnPoint.transform.position, rotate);

        Bullet bullet = _instanceBulletPrefab.GetComponent<Bullet>();

        if (bullet == null)
            return;

        bullet.InitializeBullet(_spawnPoint.transform.position, WeaponConfig.WeaponStatsConfig.RangeShooting);

        Debug.Log("Shooting and delayBeforeFiring = " + _delayBeforeFiring + " / current magazine capacity = " + _currentMagazineCapacity);
    }

    private Quaternion GetDirection()
    {
        Quaternion rotate = Quaternion.identity;
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            Vector3 point = hitInfo.point;
            Vector3 direction = (point - _spawnPoint.transform.position).normalized;
            direction.y = 0;

            rotate = Quaternion.LookRotation(direction);

            return rotate;
        }

        return rotate;
    }

}
