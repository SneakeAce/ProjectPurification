using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBullet : MonoBehaviour
{

    // ����� ����� ��� ������� ���� ����.

    [SerializeField] private GameObject _bulletPref;
    [SerializeField] private GameObject _spawPoint;
    private GameObject _instanceBulletPref;


    public Bullet SpawnBullets()
    {
        _instanceBulletPref = Instantiate(_bulletPref, _spawPoint.transform.position, _spawPoint.transform.rotation);

        Bullet bullet = _instanceBulletPref.GetComponent<Bullet>();

        if (bullet == null)
            return null;

        return bullet;
    }

}
