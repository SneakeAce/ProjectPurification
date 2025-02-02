using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private CharacterBootstrap _characterBootstrap;
    [SerializeField] private EnemyBootstrap _enemyBootstrap;
    [SerializeField] private UIBootstrap _uiBootstrap;
    [SerializeField] private CreatedPoolBarrierSystem _createdPoolBarrierSystem;

    public void Initialization()
    {
        _characterBootstrap.Initialization();
        _uiBootstrap.Initialization();
        _enemyBootstrap.Initialization();
        _createdPoolBarrierSystem.Initialization();
    }
}
