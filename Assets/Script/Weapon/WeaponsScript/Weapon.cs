using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponConfig _weaponConfig;

    public WeaponConfig WeaponConfig => _weaponConfig;

    protected virtual void Update()
    {
        // �������� ����� ��� �������� � ������� ��������, ���������� ��������� � ���������� ��������.
    }

    protected abstract IEnumerator PrepareWeaponToShootingJob(); // ��� �������� ���������� ������ � ��������
    protected abstract void Shooting(); // ��� �������� � �������� ��������
    protected abstract IEnumerator ReloadingJob(); // ��� �������� ����������� 

}
