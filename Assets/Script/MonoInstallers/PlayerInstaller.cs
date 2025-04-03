using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private PlayerConfig _playerConfig;

    public override void InstallBindings()
    {
        BindPlayerConfig();

        BindPlayerComponents();

        CreatePlayer();
    }

    private void BindPlayerConfig()
    {
        Container.Bind<PlayerConfig>().FromInstance(_playerConfig).AsSingle();
    }

    private void BindPlayerComponents()
    {
        Container.Bind<CharacterHealth>().AsSingle().NonLazy();
        Container.Bind<MoveComponent>().AsSingle().NonLazy();
    }

    private void CreatePlayer()
    {
        if (_playerConfig == null)
        {
            Debug.LogWarning("Конфиг не найден!");
            return;
        }

        Character character = Container.InstantiatePrefabForComponent<Character>(_playerConfig.PlayerPrefab, 
            _playerConfig.SpawnCoordinate, Quaternion.identity, null);

        Container.BindInterfacesAndSelfTo<Character>().FromInstance(character).AsSingle().NonLazy();
    }

}

