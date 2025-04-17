using System.Collections;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    protected LayerMask _targetLayer;
    protected EnemyCharacter _enemyCharacter;
    protected Character _target;

    protected float _baseDamage;
    protected float _reloadingTime;
    protected float _attackZoneRadius;

    protected bool _isCanAttack = true;
    protected bool _isAttacking;
    protected bool _isCanceled;

    protected Animator _enemyAnimator;

    protected Coroutine _reloadingCoroutine;

    public abstract IEnumerator ReloadingJob(float time);
    public abstract void ResetAttack();

    public void Initialization(EnemyCharacter enemyCharacter, EnemyConfig config)
    {
        _enemyCharacter = enemyCharacter;

        _targetLayer = config.AttackCharacteristicsEnemy.TargetLayer;
        _baseDamage = config.AttackCharacteristicsEnemy.BaseDamage;
        _reloadingTime = config.AttackCharacteristicsEnemy.ReloadingTime;
        _attackZoneRadius = config.AttackCharacteristicsEnemy.RadiusAttack;

        _enemyAnimator = enemyCharacter.Animator;
    }
}
