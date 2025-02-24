using UnityEngine;
using UnityEngine.AI;

public class EnemyCharacter : MonoBehaviour, IUnit, IEnemy, IPoolable
{
    private IBehavioralPattern _behavioralPattern;

    [SerializeField] private EnemyConfig _enemyConfig;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private GameObject _holderAttackLogic;
    [SerializeField] private EnemyHealth _health;
    [SerializeField] private Attack _attackEnemy;

    private Rigidbody _rigidbody;
    private Collider _collider;
    private Animator _animator;

    private ObjectPool<EnemyCharacter> _pool;

    public float MoveSpeed => _enemyConfig.ÑharacteristicsEnemy.MoveSpeed;
    public int MaxCountOnCurrentScene => _enemyConfig.ÑharacteristicsEnemy.MaxCountOnCurrentScne;
    public EnemyType EnemyType => _enemyConfig.ÑharacteristicsEnemy.EnemyType;
    public EnemyCharacter CharacterEnemy => this;
    public EnemyHealth EnemyHealth => _health;
    public Animator EnemyAnimator => Animator;
    public Rigidbody EnemyRigidbody => Rigidbody;
    public Transform Transform => transform;
    public NavMeshAgent NavMeshAgent => _agent;
    public Rigidbody Rigidbody => _rigidbody;
    public Collider Collider => _collider;
    public Animator Animator => _animator;

    public void Initialize()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _animator = GetComponent<Animator>();

        _attackEnemy.Initialization();
        EnemyHealth.Initialize(this);
    }

    public void SetPool<T>(ObjectPool<T> pool) where T : MonoBehaviour
    {
        _pool = pool as ObjectPool<EnemyCharacter>;
    }

    public void ReturnToPool(EnemyHealth enemyHealth)
    {
        _pool?.ReturnPoolObject(this);

        _health.CurrentValue = 0;
        _pool = null;
    }

    public void TriggerAttack()
    {
        BaseZombieAttack attack = _holderAttackLogic.GetComponent<BaseZombieAttack>();

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
