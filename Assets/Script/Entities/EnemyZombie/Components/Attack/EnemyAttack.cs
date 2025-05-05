using System.Collections;
using UnityEngine;
using Zenject;

public abstract class EnemyAttack : MonoBehaviour
{
    protected LayerMask _targetLayer;
    protected IEnemy _enemyCharacter;
    protected ICharacter _target;
    protected AttackType _attackType;

    protected float _baseDamage;
    protected float _reloadingTime;
    protected float _attackZoneRadius;

    protected bool _isCanAttack = true;
    protected bool _isAttacking;
    protected bool _isCanceled;

    protected Animator _enemyAnimator;

    protected EnemySearchTargetSystem _enemySearchTargetSystem;

    protected Coroutine _reloadingCoroutine;

    private EnemySearchTargetConfig _searchTargetConfig;
    private CoroutinePerformer _coroutinePerformer;

    [Inject]
    private void Construct(EnemySearchTargetConfig searchTargetConfig, CoroutinePerformer coroutinePerformer)
    {
        Debug.Log("EnemyAttack / COnstruct");
        _searchTargetConfig = searchTargetConfig;
        _coroutinePerformer = coroutinePerformer;
    }

    public abstract IEnumerator ReloadingJob(float time);
    public abstract void ResetAttack();

    public void Initialization(IEnemy enemyCharacter, EnemyConfig config)
    {
        _enemySearchTargetSystem = new EnemySearchTargetSystem(_searchTargetConfig, _coroutinePerformer);
        _enemySearchTargetSystem.Start(enemyCharacter);

        _enemyCharacter = enemyCharacter;

        _attackType = config.AttackCharacteristics.AttackType;
        _targetLayer = config.AttackCharacteristics.TargetLayer;
        _baseDamage = config.AttackCharacteristics.BaseDamage;
        _reloadingTime = config.AttackCharacteristics.ReloadingTime;
        _attackZoneRadius = config.AttackCharacteristics.RadiusAttack;

        _enemyAnimator = enemyCharacter.Animator;
    }
}
