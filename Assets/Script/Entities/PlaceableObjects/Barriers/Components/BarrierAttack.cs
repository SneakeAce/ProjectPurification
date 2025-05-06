using System.Collections;
using UnityEngine;

public class BarrierAttack
{
    private const int MaxTargetsInBuffer = 16;

    private const float MinDelayToCheck = 0.2f;
    private const float MaxDelayToCheck = 1.0f;

    private BarrierConfig _config;
    private CoroutinePerformer _coroutinePerformer;

    private int _countTargetsForAttacking;
    private Collider[] _bufferTargets;

    private float _currentDamage;    
    private float _startDelayBetweenAttack;
    private float _delayBetweenAttack;

    private LayerMask _enemyLayer;
    private AttackType _attackType;

    private Coroutine _attackCoroutine;
    private IBarrier _barrier;

    public BarrierAttack(CoroutinePerformer coroutinePerformer)
    {
        _coroutinePerformer = coroutinePerformer;
    }

    public void Initialization(BarrierConfig config, IBarrier barrier)
    {
        _barrier = barrier;
        _config = config;

        StartWork();
    }

    private void StartWork()
    {
        _attackType = _config.AttackCharacteristics.AttackType;
        _currentDamage = _config.AttackCharacteristics.BaseDamage;
        _startDelayBetweenAttack = _config.AttackCharacteristics.BaseDelayBetweenAttack;
        _enemyLayer = _config.AttackCharacteristics.EnemyLayer;
        _countTargetsForAttacking = _config.AttackCharacteristics.BaseCountTargetForAttacking;

        _bufferTargets = new Collider[MaxTargetsInBuffer];

        _delayBetweenAttack = _startDelayBetweenAttack;

        _attackCoroutine = _coroutinePerformer.StartCoroutine(AttackJob());
    }

    private IEnumerator AttackJob()
    {        
        while (_barrier.Transform.gameObject.activeSelf == true)
        {
            yield return new WaitForSeconds(Random.Range(MinDelayToCheck, MaxDelayToCheck));

            int targets = Physics.OverlapSphereNonAlloc(_barrier.Transform.position,
                _config.AttackCharacteristics.BaseRadiusAttack, 
                _bufferTargets, _enemyLayer);

            Debug.Log($"BarrierAttack targets = {targets}");

            int maxCountTargetsForAttacking = Mathf.Min(targets, _countTargetsForAttacking);

            for (int i = 0; i < maxCountTargetsForAttacking; i++)
            {
                Collider targetCol = _bufferTargets[i];

                Debug.Log($"BarierAttack for cycle. targetCol = {targetCol}");

                if (targetCol == null)
                    continue;

                DamageDeal(targetCol);

                yield return new WaitForSeconds(_delayBetweenAttack);
            }
        }
    }

    private void DamageDeal(Collider target)
    {
        IEnemy enemyTarget = target.GetComponent<IEnemy>();

        if (enemyTarget == null)
            return;

        DamageData damage = new DamageData(_attackType, _currentDamage);

        enemyTarget.CharacterEnemy.EnemyHealth.TakeDamage(damage);
    }
}
