using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    [SerializeField] private CoroutinePerformer _coroutinePerformerPrefab;
    [SerializeField] private InstantiateAndDestroyGameObjectPerformer _objectPerformer;

    public override void InstallBindings()
    {
        BindPlayerInput();

        BindPerformers();
    }

    private void BindPlayerInput()
    {
        Container.Bind<PlayerInput>().AsSingle();
    }

    private void BindPerformers()
    {
        Container.Bind<CoroutinePerformer>().FromComponentInNewPrefab(_coroutinePerformerPrefab).AsSingle();

        Container.Bind<InstantiateAndDestroyGameObjectPerformer>().FromComponentInNewPrefab(_objectPerformer).AsSingle();
    }

}
