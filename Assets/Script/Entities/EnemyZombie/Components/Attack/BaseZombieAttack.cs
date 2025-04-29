public class BaseZombieAttack : EnemyMeleeAttack
{
    public override void Attack(ICharacter character)
    {
        if (character == null) 
            return;

        CharacterHealth characterHealth = character.CharacterHealth;

        characterHealth.DamageTaken(_baseDamage);

        if (_reloadingCoroutine != null)
        {
            StopCoroutine( _reloadingCoroutine);
            _reloadingCoroutine = null;
        }

        _reloadingCoroutine = StartCoroutine(ReloadingJob(_reloadingTime));
    }
}
