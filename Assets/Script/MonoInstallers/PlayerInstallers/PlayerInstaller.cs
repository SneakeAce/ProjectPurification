using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private PlayerConfig _playerConfig;

    public override void InstallBindings()
    {
        BindPlayerConfig();

        CreatePlayer();
    }

    private void BindPlayerConfig()
    {
        Container.Bind<PlayerConfig>().FromInstance(_playerConfig).AsSingle();
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

        Container.BindInterfacesAndSelfTo<Character>().FromInstance(character).AsSingle();
    }

}

