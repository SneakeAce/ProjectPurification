using UnityEngine;
using UnityEngine.TextCore.Text;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private PlayerConfig _playerConfig;
    [SerializeField] private CharacterBootstrap _bootstrap;

    private Character _character;

    public override void InstallBindings()
    {
        BindPlayerConfig();

        BindPlayerComponents();

        CreatePlayer();
    }

    private void BindPlayerConfig()
    {
        Container.Bind<PlayerConfig>().FromInstance(_playerConfig).AsSingle();

        Debug.Log("��������� ������");
    }

    private void BindPlayerComponents()
    {
        Container.Bind<CharacterHealth>().AsSingle().NonLazy();
        Container.Bind<MoveComponent>().AsSingle().NonLazy();

        Debug.Log("��������� �� � ������������");
    }

    private void CreatePlayer()
    {
        if (_playerConfig == null)
        {
            Debug.Log("������ �� ������!");
            return;
        }

        Character character = Container.InstantiatePrefabForComponent<Character>(_playerConfig.PlayerPrefab, 
            _playerConfig.SpawnCoordinate, Quaternion.identity, null);

        Container.BindInterfacesAndSelfTo<Character>().FromInstance(character).AsSingle().NonLazy();
    }

}

