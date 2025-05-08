using UnityEngine;
using Zenject;

public class DamageSystemInstaller : MonoInstaller
{
    [SerializeField] private TextAsset _damageMatrixCsvFile;

    public override void InstallBindings()
    {
        BindDamageCoefficientProvider();

        BindDamageCalculator();
    }

    private void BindDamageCalculator()
    {
        Container.Bind<IDamageCalculator>().To<DamageCalculator>().AsSingle();
    }

    private void BindDamageCoefficientProvider()
    {
        Container.Bind<IDamageCoefficientProvider>()
            .To<DamageCoefficientProvider>()
            .AsSingle()
            .WithArguments(_damageMatrixCsvFile).NonLazy();
    }
}
