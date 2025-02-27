using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private CharacterBootstrap _characterBootstrap;
    [SerializeField] private PoolCreator _createdPoolsBootstrap;
    [SerializeField] private UIBootstrap _uiBootstrap;
    [SerializeField] private EnemyBootstrap _enemyBootstrap;

    public void Initialization() 
    { 
        //_characterBootstrap.Initialization();
        _createdPoolsBootstrap.Initialization();
        _uiBootstrap.Initialization();
        _enemyBootstrap.Initialization();
    }
}
