using UnityEngine;
using Zenject; 

public class Turret : MonoBehaviour, ITurret, IPoolable
{
    // Сделать Turret абстрактным классом и наследовать от него другие классы турелей.
    protected GameObject _bodyTurret;

    protected IFactory<Bullet, BulletConfig, BulletType> _bulletFactory;

    private TurretConfig _turretConfig;
    private TurretHealth _turretHealth;

    private AutomaticTurretWeapon _turretWeapon;
    private ObjectPool<Turret> _pool;

    public Transform Transform => transform;
    public Collider Collider => GetComponent<Collider>();
    public AutomaticTurretWeapon TurretWeapon => _turretWeapon;
    public GameObject BodyTurret => this.gameObject;
    public Animator Animator => throw new System.NotImplementedException();
    public Rigidbody Rigidbody => throw new System.NotImplementedException();
    public IEntityHealth EntityHealth => _turretHealth;

    [Inject]
    private void Construct(IFactory<Bullet, BulletConfig, BulletType> bulletFactory, TurretHealth health,
        AutomaticTurretWeapon turretWeapon)
    {
        _bulletFactory = bulletFactory;

        _turretHealth = health;
        _turretHealth.EntityDied += ReturnToPool;

        _bodyTurret = GetComponentInChildren<BodyTurret>().transform.gameObject; 

        _turretWeapon = turretWeapon;
    }

    public void SetComponents(TurretConfig turretConfig)
    {
        _turretConfig = turretConfig;

        Initialize();
    }

    public void SetPool<T>(ObjectPool<T> pool) where T : MonoBehaviour
    {
        _pool = pool as ObjectPool<Turret>;
    }

    public void ReturnToPool(IEntity entity)
    {
        _pool?.ReturnPoolObject(this);

        _pool = null;
    }

    private void Initialize()
    {
        _turretHealth.Initialization(this, _turretConfig);
        _turretWeapon.Initialization(this, _turretConfig, _bodyTurret);
    }

}