using System.Collections;
using UnityEngine;

public abstract class EnemyMeleeAttack : Attack
{
    public abstract void Attack(ICharacter character);

    public override IEnumerator ReloadingJob(float time)
    {
        yield return new WaitForSeconds(time);

        ResetAttack();
    }

    public override void ResetAttack()
    {
        _isCanAttack = true;
        _isAttacking = false;
        _isCanceled = false;
    }

    public void AnimationAttack()
    {
        Attack(_target);
    }

    private void OnTriggerStay(Collider collider)
    {
        // Переделать проверку по слою.
        if (collider.gameObject.layer == 6 && _isCanAttack)
        {
            if (collider.gameObject.TryGetComponent(out Character character))
            {
                _target = character;

                if (_isAttacking == false)
                {
                    _isCanAttack = false;
                    _isAttacking = true;

                    _enemyAnimator.SetTrigger("Attacking");
                }
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.layer == 6)
        {
            CancelAttack();
        }
    }

    private void CancelAttack()
    {
        if (_reloadingCoroutine == null)
            _reloadingCoroutine = StartCoroutine(ReloadingJob(_reloadingTime));

        // ПЕРЕДЕЛАТЬ!!!
        _enemyAnimator.CrossFade("ZombieIdle", 0.1f);
        //_animator.StopPlayback();
    }
}
