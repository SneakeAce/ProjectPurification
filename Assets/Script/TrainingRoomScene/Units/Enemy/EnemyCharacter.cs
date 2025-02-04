using UnityEngine;
using UnityEngine.AI;

public class EnemyCharacter : Unit, IEnemy, IPoolable
{
    private IBehavioralPattern _behavioralPattern;

    [SerializeField] private EnemyType enemyType;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private GameObject _holderAttackLogic;
    [SerializeField] private EnemyHealth _health;
    [SerializeField] private Attack _attackEnemy;

    private ObjectPool<EnemyCharacter> _pool;

    public float MoveSpeed => _moveSpeed;
    public Animator EnemyAnimator => Animator;
    public Rigidbody EnemyRigidbody => Rigidbody;
    public Transform Transform => transform;
    public NavMeshAgent NavMeshAgent => _agent;
    public EnemyCharacter CharacterEnemy => this;
    public EnemyHealth EnemyHealth => _health;
    public EnemyType EnemyType => enemyType;

    public void Initialize()
    {
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
