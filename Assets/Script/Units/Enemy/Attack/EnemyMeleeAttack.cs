using System;
using System.Collections;
using UnityEngine;

public abstract class EnemyMeleeAttack : Attack
{
    [SerializeField] private SphereCollider _attackZone;

    private Coroutine _checkDistanceCoroutine;

    /* ======================================================================================================= */

    public abstract void MeleeAttack(Character character);

    public override IEnumerator ReloadingJob(float time)
    {
        while (time > 0)
        {
            time -= Time.deltaTime;
            yield return null;
        }
        
        ResetVariables();

        StopCoroutine(_reloadingCoroutine);
        _reloadingCoroutine = null;
    }

    public void AnimationAttack()
    {
        MeleeAttack(_target);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && _isCanAttack)
        {
            if (other.gameObject.TryGetComponent(out Character character))
            {
                _target = character;

                if (_isAttacking == false)
                {
                    _isCanAttack = false;
                    _isAttacking = true;

                    if (_checkDistanceCoroutine != null)
                    {
                        StopCoroutine(_checkDistanceCoroutine);
                        _checkDistanceCoroutine = null;
                    }

                    _checkDistanceCoroutine = StartCoroutine(CheckDistanceJob());

                    _animator.SetTrigger("Attacking");
                }
            }
        }
    }

    private IEnumerator CheckDistanceJob()
    {
        while (_isAttacking && _isCanceled == false)
        {
            if (Vector3.Distance(transform.position, _target.transform.position) > _attackZone.radius)
            {
                CancelAttack();
            }
            yield return null;
        }
    }

    private void CancelAttack()
    {
        if (_reloadingCoroutine == null)
            _reloadingCoroutine = StartCoroutine(ReloadingJob(_reloadingTime));

        _animator.StopPlayback();
    }

    private void ResetVariables()
    {
        _isCanAttack = true;
        _isAttacking = false;
        _isCanceled = false;
    }
}
