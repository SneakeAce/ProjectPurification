using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private PlayerConfig _playerConfig;
    [SerializeField] private CharacterBootstrap _bootstrap;
    [SerializeField] private PoolCreator _poolCreator;


    public override void InstallBindings()
    {
        BindPlayerConfig();

        CreatePlayer();

        // Убрать бутстраппер отсюда!!!

        Container.Bind<CharacterBootstrap>().FromInstance(_bootstrap).AsSingle();
    }

    private void BindPlayerConfig()
    {
        Container.Bind<PlayerConfig>().FromInstance(_playerConfig).AsSingle();

        // Бинды компонентов (!!!ПЕРЕДЕЛАТЬ!!!)
        Container.Bind<CharacterHealth>().AsSingle().NonLazy();
        Container.Bind<MoveComponent>().AsSingle().NonLazy();

        Debug.Log("Привязали конфиг, и хп, и передвижение");
    }

    private void CreatePlayer()
    {
        if (_playerConfig == null)
        {
            Debug.Log("Конфиг не найден!");
            return;
        }

        Character character = Container.InstantiatePrefabForComponent<Character>(_playerConfig.PlayerPrefab, _playerConfig.SpawnCoordinate, Quaternion.identity, null);

        Container.BindInterfacesAndSelfTo<Character>().FromInstance(character).AsSingle().NonLazy();

        Weapon weapon = character.GetComponentInChildren<Weapon>();

        Container.Bind<PoolCreator>().FromInstance(_poolCreator).AsSingle();

        Container.Bind<Weapon>().FromInstance(weapon).AsSingle().NonLazy();

    }
}

