using System.Collections;
using UnityEngine;
using Zenject;

public abstract class EnemyAttack : MonoBehaviour
{
    protected IEnemy _enemyCharacter;
    protected IEntity _target;
    protected AttackType _attackType;

    protected float _baseDamage;
    protected float _reloadingTime;
    protected float _attackZoneRadius;

    protected bool _isCanAttack;
    protected bool _isAttacking;
    protected bool _isCanceled;

    protected Animator _enemyAnimator;

    protected EnemySearchTargetSystem _enemySearchTargetSystem;

    protected Coroutine _reloadingCoroutine;
    protected Coroutine _attackCoroutine;

    [Inject]
    private void Construct(EnemySearchTargetSystem enemySearchTargetSystem)
    {
        _enemySearchTargetSystem = enemySearchTargetSystem;
    }

    public abstract IEnumerator AttackJob();
    public abstract IEnumerator ReloadingJob(float time);
    public abstract void ResetAttack();
    public abstract void CancelAttack();

    public void Initialization(IEnemy enemyCharacter, EnemyConfig config)
    {
        _enemySearchTargetSystem.Start(enemyCharacter, config);

        SubscribingEvents();

        _enemyCharacter = enemyCharacter;

        _attackType = config.AttackCharacteristics.AttackType;
        _baseDamage = config.AttackCharacteristics.BaseDamage;
        _reloadingTime = config.AttackCharacteristics.BaseReloadingTime;
        _attackZoneRadius = config.AttackCharacteristics.BaseRadiusAttack;

        _enemyAnimator = enemyCharacter.Animator;

        _isCanAttack = true;
    }

    private void SubscribingEvents()
    {
        _enemySearchTargetSystem.TargetFound += OnTargetFound;

        _enemySearchTargetSystem.TargetDisapperead += OnTargetDisapperead;
    }

    private void OnTargetFound(IEntity target)
    {
        _target = target;

        _attackCoroutine = StartCoroutine(AttackJob());
    }

    private void OnTargetDisapperead()
    {
        _target = null;
        CancelAttack();
    }

    private void OnDisable()
    {
        if (_enemySearchTargetSystem == null)
            return;

        _enemySearchTargetSystem.TargetFound -= OnTargetFound;

        _enemySearchTargetSystem.TargetDisapperead -= OnTargetDisapperead;
    }
}
