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

        // ������ ����������� ������!!!

        Container.Bind<CharacterBootstrap>().FromInstance(_bootstrap).AsSingle();
    }

    private void BindPlayerConfig()
    {
        Container.Bind<PlayerConfig>().FromInstance(_playerConfig).AsSingle();

        // ����� ����������� (!!!����������!!!)
        Container.Bind<CharacterHealth>().AsSingle().NonLazy();
        Container.Bind<MoveComponent>().AsSingle().NonLazy();

        Debug.Log("��������� ������, � ��, � ������������");
    }

    private void CreatePlayer()
    {
        if (_playerConfig == null)
        {
            Debug.Log("������ �� ������!");
            return;
        }

        Character character = Container.InstantiatePrefabForComponent<Character>(_playerConfig.PlayerPrefab, _playerConfig.SpawnCoordinate, Quaternion.identity, null);

        Container.BindInterfacesAndSelfTo<Character>().FromInstance(character).AsSingle().NonLazy();

        Weapon weapon = character.GetComponentInChildren<Weapon>();

        Container.Bind<PoolCreator>().FromInstance(_poolCreator).AsSingle();

        Container.Bind<Weapon>().FromInstance(weapon).AsSingle().NonLazy();

    }
}

