using System.Collections;
using UnityEngine;

public abstract class EnemyMeleeAttack : EnemyAttack
{
    private const float DelayBetweenCheckDistance = 0.5f;

    public abstract void MeleeAttack(IEntity character);

    public override IEnumerator ReloadingJob(float time)
    {
        yield return new WaitForSeconds(time);

        ResetAttack();
    }

    public override void ResetAttack()
    {
        _isAttacking = false;
        _isCanceled = false;
    }

    public override IEnumerator AttackJob()
    {
        while (_target != null)
        {
            if (_isCanceled)
                yield break;

            yield return new WaitForSeconds(DelayBetweenCheckDistance);

            _isCanAttack = TargetInRadiusAttack();

            if (_isCanAttack && _isAttacking == false)
            {
                _isAttacking = true;

                _enemyAnimator.SetTrigger("Attacking");
            }
        }
    }

    public override void CancelAttack()
    {
        _isCanceled = true;

        if (_reloadingCoroutine == null)
            _reloadingCoroutine = StartCoroutine(ReloadingJob(_reloadingTime));

        // ÏÅĞÅÄÅËÀÒÜ!!!
        _enemyAnimator.CrossFade("ZombieIdle", 0.1f);

        // Çàêıøèğîâàòü íàçâàíèå àíèìàöèè ñ ïîìîùü StringToHash.
    }

    public void AnimationAttack()
    {
        MeleeAttack(_target);
    }

    private bool TargetInRadiusAttack()
    {
        if (_target == null)
            return false;

        float sqrDistanceToTarget = (_target.Transform.position - _enemyCharacter.Transform.position).sqrMagnitude;
        float sqrRadiusAttack = _attackZoneRadius * _attackZoneRadius;

        if (sqrDistanceToTarget > sqrRadiusAttack)
            return false;

         return true;
    }
}
