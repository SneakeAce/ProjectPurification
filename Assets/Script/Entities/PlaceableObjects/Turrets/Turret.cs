using UnityEngine;
using Zenject; 

public class Turret : MonoBehaviour, ITurret
{
    private TurretConfig _turretConfig;
    private TurretHealth _turretHealth;
    private TurretSearchTargetSystem _searchTargetSystem;
    private AutomaticTurretWeapon _turretWeapon;

    protected CoroutinePerformer _coroutinePerformer;
    protected GameObject _bodyTurret;

    protected IFactory<Bullet, BulletConfig, BulletType> _bulletFactory;

    public Transform Transform => transform;
    public Animator Animator => throw new System.NotImplementedException();
    public Rigidbody Rigidbody => throw new System.NotImplementedException();
    public Collider Collider => throw new System.NotImplementedException();
    public AutomaticTurretWeapon TurretWeapon => _turretWeapon;
    public GameObject BodyTurret => this.gameObject;

    [Inject]
    private void Construct(IFactory<Bullet, BulletConfig, BulletType> bulletFactory, CoroutinePerformer coroutinePerformer, TurretHealth health)
    {
        _bulletFactory = bulletFactory;

        _coroutinePerformer = coroutinePerformer;

        _turretHealth = health;

        _bodyTurret = GetComponentInChildren<BodyTurret>().transform.gameObject; 
    }

    public void SetComponents(TurretConfig turretConfig)
    {
        _turretConfig = turretConfig;

        Initialize();
    }

    private void Initialize()
    {
        _searchTargetSystem = new TurretSearchTargetSystem(_coroutinePerformer);

        _turretWeapon = new AutomaticTurretWeapon(this, _turretConfig, _searchTargetSystem, 
            _bulletFactory, _coroutinePerformer, _bodyTurret);

        _turretHealth.Initialization(this, _turretConfig);
    }
}