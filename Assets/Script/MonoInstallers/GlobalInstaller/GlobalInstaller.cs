using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    [SerializeField] private CoroutinePerformer _coroutinePerformerPrefab;
    [SerializeField] private InstantiateAndDestroyGameObjectPerformer _objectPerformer;
    [SerializeField] private TextAsset _damageMatrixCsvFile;

    public override void InstallBindings()
    {
        BindPlayerInput();

        BindPerformers();

        BindDamageCoefficientProvider();
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

    private void BindDamageCoefficientProvider()
    {
        Container.Bind<IDamageCoefficientProvider>()
            .To<DamageCoefficientProvider>()
            .AsSingle()
            .WithArguments(_damageMatrixCsvFile).NonLazy();

        UnityEngine.Debug.Log("DamageCoefficientProvider bound with CSV file: " + _damageMatrixCsvFile); 
    }
}
