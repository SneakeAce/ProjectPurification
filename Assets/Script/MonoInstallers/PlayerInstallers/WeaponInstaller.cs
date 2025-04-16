using UnityEngine;
using Zenject;

public class WeaponInstaller : MonoInstaller
{
    [SerializeField] private InputForWeaponAttackHandler _inputForWeapon;
    [SerializeField] private SwitchWeaponHandler _weaponHandler;

    public override void InstallBindings()
    {
        BindWeaponFactory();

        BindFiringModeStrategies();

        BindInputForWeapon();
    }

    private void BindWeaponFactory()
    {
        Container.Bind<IWeaponFactory>().To<WeaponFactory>().AsSingle();
    }

    private void BindInputForWeapon()
    {
        Container.Bind<InputForWeaponAttackHandler>().FromInstance(_inputForWeapon).AsSingle();

        Container.Bind<SwitchWeaponHandler>().FromInstance(_weaponHandler).AsSingle(); // !!!”¡–¿“‹ œŒ“ŒÃ!!!
    }

    private void BindFiringModeStrategies()
    {
        Container.Bind<IFiringModeStrategy>().To<AutoFiringMode>().AsTransient();
        Container.Bind<IFiringModeStrategy>().To<SingleFiringMode>().AsTransient();
        Container.Bind<IFiringModeStrategy>().To<BurstFiringMode>().AsTransient();
    }
}
