using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private EnemyBootstrap _enemyBootstrap;

    public void Initialization() 
    { 
        _enemyBootstrap.Initialization();
    }
}
