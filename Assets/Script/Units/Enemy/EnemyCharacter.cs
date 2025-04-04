using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class EnemyCharacter : MonoBehaviour, IUnit, IEnemy, IPoolable
{
    private const string NameAttackHolder = "AttackHolder";

    private Transform _holderAttackLogic;

    private IBehavioralPattern _behavioralPattern;
    private EnemyConfig _enemyConfig;
    private EnemyHealth _health;
    private Attack _attackEnemy;

    private NavMeshAgent _agent;
    private Rigidbody _rigidbody;
    private Collider _collider;
    private Animator _animator;

    private ObjectPool<EnemyCharacter> _pool;

    [Inject]
    private void Construct(EnemyConfig enemyConfig, EnemyHealth health)
    {
        _enemyConfig = enemyConfig;
        Debug.Log("EnemyCharacter / Construct / enemyConfig = " + _enemyConfig);
        _health = health;
    }

    public float MoveSpeed => _enemyConfig.CharacteristicsEnemy.MoveSpeed;
    public EnemyType EnemyType => _enemyConfig.CharacteristicsEnemy.EnemyType;
    public Transform Transform => transform;
    public NavMeshAgent NavMeshAgent => _agent;
    public Animator Animator => _animator;
    public EnemyCharacter CharacterEnemy => this;
    public EnemyHealth EnemyHealth => _health;
    public Rigidbody Rigidbody => _rigidbody;
    public Collider Collider  => _collider;

    public void SetAttackComponent(Attack attackEnemy)
    {
        _attackEnemy = attackEnemy;
    }

    public void Initialize()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();

        _holderAttackLogic = transform.Find(NameAttackHolder);

        _attackEnemy.Initialization(this, _enemyConfig);

        EnemyHealth.Initialize(this, _enemyConfig);
    }

    public void SetPool<T>(ObjectPool<T> pool) where T : MonoBehaviour
    {
        _pool = pool as ObjectPool<EnemyCharacter>;
    }

    public void ReturnToPool(EnemyHealth enemyHealth)
    {
        _pool?.ReturnPoolObject(this);

        enemyHealth.CurrentValue = 0;
        _pool = null;
    }

    public void TriggerAttack()
    {
        BaseZombieAttack attack = _holderAttackLogic.gameObject.GetComponent<BaseZombieAttack>();

        attack.AnimationAttack();
    }

    public void SetBehavioralPattern(IBehavioralPattern behavioralPattern)
    {
        _behavioralPattern?.StopMove();

        _behavioralPattern = behavioralPattern;

        _behavioralPattern.StartMove();
    }

    private void Update()
    {
        if (_behavioralPattern != null)
            _behavioralPattern.Update();
    }
}
