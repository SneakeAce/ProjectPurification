using System.Collections;
using UnityEngine;

public class SingleFiringMode : IFiringModeStrategy
{
    public FiringMode FiringMode => FiringMode.SingleFireMode;

    public IEnumerator FiringWeaponJob(IWeapon weapon)
    {
        if (weapon == null)
        {
            Debug.LogWarning("Weapon not founded in BurstFiringMode!");
            yield break;
        }

        if (weapon.IsCanFiring == false)
            yield break;

        weapon.StartShoot();

        yield return new WaitForSeconds(weapon.DelayBeforeFiring);

        weapon.StopShoot();
    }
}
