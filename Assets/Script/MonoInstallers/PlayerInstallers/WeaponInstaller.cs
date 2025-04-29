using UnityEngine;
using Zenject;

public class WeaponInstaller : MonoInstaller
{
    [SerializeField] private InputWeaponHandler _inputForWeapon;
    [SerializeField] private WeaponManager _weaponManager;

    public override void InstallBindings()
    {
        BindWeaponFactory();

        BindFiringModeStrategies();

        BindInputForWeapon();
    }

    private void BindWeaponFactory()
    {
        Container.Bind<IWeaponFactory>().To<WeaponFactory>().AsSingle();

        Container.Bind<WeaponSwitcher>().AsSingle(); // ”·‡Ú¸!!!!
    }

    private void BindInputForWeapon()
    {
        Container.Bind<InputWeaponHandler>().FromInstance(_inputForWeapon).AsSingle();

        Container.Bind<WeaponManager>().FromInstance(_weaponManager).AsSingle(); // !!!”¡–¿“‹ œŒ“ŒÃ!!!
    }

    private void BindFiringModeStrategies()
    {
        Container.Bind<IFiringModeStrategy>().To<AutoFiringMode>().AsTransient();
        Container.Bind<IFiringModeStrategy>().To<SingleFiringMode>().AsTransient();
        Container.Bind<IFiringModeStrategy>().To<BurstFiringMode>().AsTransient();
    }
}
