using UnityEngine;
using Zenject;

public class EntitiesHealthInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindEntitiesHealth();
    }

    private void BindEntitiesHealth()
    {
        Container.Bind<TurretHealth>().AsTransient();
        Container.Bind<EnemyHealth>().AsTransient();
        Container.Bind<CharacterHealth>().AsTransient();
        Container.Bind<PlaceableObjectHealth>().AsTransient();
    }   

}
