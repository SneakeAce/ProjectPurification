using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _spawnPoint;
    [SerializeField] private float _offsetBulletRotation;
    private GameObject _instanceBulletPrefab;

    private Vector3 _directionToMousePosition;

    protected override void Update()
    {
        base.Update();

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            Shooting();
        }
    }

    protected override IEnumerator PrepareWeaponToShootingJob()
    {
        throw new System.NotImplementedException();
    }

    protected override IEnumerator ReloadingJob()
    {
        throw new System.NotImplementedException();
    }

    protected override void Shooting()
    {
        _directionToMousePosition = MousePosition();
        _directionToMousePosition.y = 0;
        SpawnBullet();

        Debug.Log("Shooting");
    }

    private Vector3 MousePosition()
    {
        // Сделать скрипт для камеры, в котором будет публичный метод для получения точки в мировом пространстве или что-то типо того.

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            Vector3 point = hitInfo.point;

            return point;
        }

        return Vector3.zero;
    }

    private void SpawnBullet()
    {
        // Переделать поворт пули при спавне.
        Quaternion angle = RotateBullet();

        _instanceBulletPrefab = Instantiate(_bulletPrefab, _spawnPoint.transform.position, Quaternion.Euler(angle.x, angle.y, angle.z));

        Bullet bullet = _instanceBulletPrefab.GetComponent<Bullet>();

        if (bullet == null)
            return;

        bullet.SpawnBullet(_spawnPoint.transform, _directionToMousePosition);
    }

    private Quaternion RotateBullet()
    {
        Vector3 direction = _directionToMousePosition - _spawnPoint.transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        return targetRotation;
    }

}
