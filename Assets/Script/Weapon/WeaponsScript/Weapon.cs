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
        // Написать метод для стрельбы с зажатой клавишей, одиночными встрелами и выстрелами очередью.
    }

    protected abstract IEnumerator PrepareWeaponToShootingJob(); // Для анимации подготовки оружия к стрельбе
    protected abstract void Shooting(); // Для выстрела и анимации выстрела
    protected abstract IEnumerator ReloadingJob(); // Для анимации перезарядки 

}
