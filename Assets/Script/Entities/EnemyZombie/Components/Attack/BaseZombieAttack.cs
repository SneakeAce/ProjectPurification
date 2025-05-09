public class BaseZombieAttack : EnemyMeleeAttack
{
    public override void MeleeAttack(IEntity entity)
    {
        if (entity == null) 
            return;

        IDamageable entityHealth = entity.EntityHealth;

        DamageData damage = new DamageData(_attackType, _baseDamage);

        UnityEngine.Debug.Log($"DamageData = {_attackType}, {_baseDamage}");

        entityHealth.TakeDamage(damage);

        if (_reloadingCoroutine != null)
        {
            StopCoroutine( _reloadingCoroutine);
            _reloadingCoroutine = null;
        }

        _reloadingCoroutine = StartCoroutine(ReloadingJob(_reloadingTime));
    }
}
