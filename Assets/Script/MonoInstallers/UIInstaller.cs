using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [SerializeField] private Bar _healthBar;
    [SerializeField] private Bar _bulletBar;

    public override void InstallBindings()
    {
        BindHealthBar();

        BindBulletBar();
    }

    private void BindHealthBar()
    {
        Container.Bind<Bar>().FromInstance(_healthBar).AsTransient().NonLazy();
    }

    private void BindBulletBar()
    {
        Container.Bind<Bar>().FromInstance(_bulletBar).AsTransient().NonLazy();
    }
}
