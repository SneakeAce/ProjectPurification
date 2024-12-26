using System.Collections;
using UnityEngine;

public class Rifle : Weapon
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _spawnPoint;
    private GameObject _instanceBulletPrefab;

    private Vector3 _directionToMousePosition;

    protected override void Update()
    {
        base.Update();

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
            Shooting();
        
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

        Debug.Log("Shooting");
    }

}
