using System.Diagnostics;

public class BaseZombieAttack : EnemyMeleeAttack
{
    public override void Attack(ICharacter character)
    {
        if (character == null) 
            return;

        IDamageable characterHealth = character.CharacterHealth;

        DamageData damage = new DamageData(_attackType, _baseDamage);

        UnityEngine.Debug.Log($"DamageData = {_attackType}, {_baseDamage}");

        characterHealth.TakeDamage(damage);

        if (_reloadingCoroutine != null)
        {
            StopCoroutine( _reloadingCoroutine);
            _reloadingCoroutine = null;
        }

        _reloadingCoroutine = StartCoroutine(ReloadingJob(_reloadingTime));
    }
}
