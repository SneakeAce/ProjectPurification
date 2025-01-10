using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseZombieAttack : EnemyMeleeAttack
{
    public override void MeleeAttack(Character character)
    {
        Debug.Log("Zombie Attack");
        CharacterHealth characterHealth = character.GetComponent<CharacterHealth>();

        characterHealth.DamageTaken(_damage);

        if (_reloadingCoroutine != null)
        {
            StopCoroutine( _reloadingCoroutine);
            _reloadingCoroutine = null;
        }

        _reloadingCoroutine = StartCoroutine(ReloadingJob(_reloadingTime));

    }
}
