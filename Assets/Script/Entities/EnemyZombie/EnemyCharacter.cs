using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class EnemyCharacter : MonoBehaviour, IEnemy, IPoolable
{
    private const string NameAttackHolder = "AttackHolder";
    private const string NamePatrolPointHolder = "PatrolPointsHolder";

    [Inject] private EnemyHealth _health;

    private Transform _patrolPointHolder;
    private Transform _holderAttackLogic;

    private IBehavioralPattern _behavioralPattern;
    private EnemyConfig _enemyConfig;
    private EnemyAttack _attackEnemy;
    private BehavioralPatternSwitcher _patternSwitcher;

    private NavMeshAgent _agent;
    private Rigidbody _rigidbody;
    private Collider _collider;
    private Animator _animator;

    private ObjectPool<EnemyCharacter> _pool;

    public float MoveSpeed => _enemyConfig.CharacteristicsEnemy.MoveSpeed;
    public EnemyType EnemyType => _enemyConfig.CharacteristicsEnemy.EnemyType;
    public BehavioralPatternSwitcher BehavioralPatternSwitcher => _patternSwitcher;
    public Transform PatrolPointsHolder => _patrolPointHolder;
    public Transform Transform => transform;
    public NavMeshAgent NavMeshAgent => _agent;
    public Animator Animator => _animator;
    public EnemyCharacter CharacterEnemy => this;
    public EnemyHealth EnemyHealth => _health;
    public Rigidbody Rigidbody => _rigidbody;
    public Collider Collider  => _collider;

    public void SetEnemyComponents(EnemyConfig config, EnemyAttack attackEnemy)
    {
        _enemyConfig = config;

        _attackEnemy = attackEnemy;
    }

    public void Initialize()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();

        _patternSwitcher = GetComponentInChildren<BehavioralPatternSwitcher>();

        _patrolPointHolder = transform.Find(NamePatrolPointHolder);
        _holderAttackLogic = transform.Find(NameAttackHolder);

        _health.Initialization(this, _enemyConfig);

        _attackEnemy.Initialization(this, _enemyConfig);
    }

    public void SetPool<T>(ObjectPool<T> pool) where T : MonoBehaviour
    {
        _pool = pool as ObjectPool<EnemyCharacter>;
    }

    public void ReturnToPool(IEnemy enemy)
    {
        _pool?.ReturnPoolObject(this);

        enemy.CharacterEnemy.EnemyHealth.CurrentValue = 0;
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

        _behavioralPattern?.StartMove();
    }

    private void OnDisable()
    {
        Debug.Log("EnemyCharacter / OnDisable");

        if (_behavioralPattern != null)
        {
            _behavioralPattern.StopMove();
            _behavioralPattern = null;
        }
    }

    private void Update()
    {
        if (_behavioralPattern != null)
            _behavioralPattern.Update();
    }
}
