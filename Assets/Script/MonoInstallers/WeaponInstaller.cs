using UnityEngine;
using Zenject;

public class WeaponInstaller : MonoInstaller
{
    [SerializeField] private WeaponConfig _weaponConfig;

    public override void InstallBindings()
    {
        BindWeapon();
    }

    private void BindWeapon()
    {
            Container.Bind<WeaponConfig>().FromInstance(_weaponConfig).AsSingle();

            Container.Bind<Weapon>().FromComponentInHierarchy().AsSingle().NonLazy();
    }
}
