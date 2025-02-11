using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    [SerializeField] protected LayerMask _targetMask;
    [SerializeField] protected float _damage;
    [SerializeField] protected float _reloadingTime;
    [SerializeField] protected EnemyCharacter _enemyCharacter;

    protected Character _target;

    protected bool _isCanAttack = true;
    protected bool _isAttacking;
    protected bool _isCanceled;

    protected Animator _animator;

    protected Coroutine _reloadingCoroutine;

    public abstract IEnumerator ReloadingJob(float time);

    /* ======================================================================================================= */

    public void Initialization()
    {
        _animator = _enemyCharacter.GetComponent<Animator>();
    }
}
