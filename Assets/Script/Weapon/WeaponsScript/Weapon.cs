using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponConfig _weaponConfig;

    public WeaponConfig WeaponConfig => _weaponConfig;

    protected virtual void Update()
    {
        // Написать метод для стрельбы с зажатой клавишей, одиночными встрелами и выстрелами очередью.
    }

    protected abstract IEnumerator PrepareWeaponToShootingJob(); // Для анимации подготовки оружия к стрельбе
    protected abstract void Shooting(); // Для выстрела и анимации выстрела
    protected abstract IEnumerator ReloadingJob(); // Для анимации перезарядки 

}
