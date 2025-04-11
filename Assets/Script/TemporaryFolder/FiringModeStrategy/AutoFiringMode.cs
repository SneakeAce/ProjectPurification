using System.Collections;
using UnityEngine;

public class AutoFiringMode : IFiringModeStrategy
{
    private const float DelayBeforeResetWeapon = 0.1f;

    public FiringMode FiringMode => FiringMode.AutomaticFireMode;

    public IEnumerator FiringWeaponJob(IWeapon weapon)
    {
        if (weapon == null)
        {
            Debug.LogWarning("Weapon not founded in BurstFiringMode!");
            yield break;
        }

        while (weapon.IsFiring)
        {
            weapon.StartShoot();

            yield return new WaitForSeconds(weapon.DelayBeforeFiring);
        }

        yield return new WaitForSeconds(DelayBeforeResetWeapon);

        weapon.StopShoot();
    }
}
