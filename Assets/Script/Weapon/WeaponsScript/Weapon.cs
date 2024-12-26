using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Main parameters weapon")]
    [SerializeField] protected WeaponConfig _weaponConfig;

    [Header("Delay before firing")]
    private const float MinDelayBeforeFiring = 0.1f;
    [SerializeField] private float _currentDelayBeforeFiring;

    public WeaponConfig WeaponConfig => _weaponConfig;

    protected virtual void Update()
    {
        // �������� ����� ��� �������� � ������� ��������, ���������� ��������� � ���������� ��������.
    }

    protected abstract IEnumerator PrepareWeaponToShootingJob(); // ��� �������� ���������� ������ � ��������
    protected abstract void Shooting(); // ��� �������� � �������� ��������
    protected abstract IEnumerator ReloadingJob(); // ��� �������� ����������� 

}
