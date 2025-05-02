using UnityEngine;
using Zenject;

public class WeaponInstaller : MonoInstaller
{
    [SerializeField] private InputWeaponHandler _inputForWeapon;
    [SerializeField] private WeaponManager _weaponManager;

    public override void InstallBindings()
    {
        BindFiringModeStrategies();

        BindWeaponManager();

        BindInputForWeapon();
    }

    private void BindFiringModeStrategies()
    {
        Container.Bind<IFiringModeStrategy>().To<AutoFiringMode>().AsTransient();
        Container.Bind<IFiringModeStrategy>().To<SingleFiringMode>().AsTransient();
        Container.Bind<IFiringModeStrategy>().To<BurstFiringMode>().AsTransient();
    }

    private void BindWeaponManager()
    {
        Container.Bind<WeaponSwitcher>().AsSingle();

        Container.Bind<WeaponManager>().FromInstance(_weaponManager).AsSingle();
    }

    private void BindInputForWeapon()
    {
        Container.Bind<InputWeaponHandler>().FromInstance(_inputForWeapon).AsSingle();
    }

}
