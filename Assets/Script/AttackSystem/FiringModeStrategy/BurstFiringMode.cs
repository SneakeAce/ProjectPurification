using System.Collections;
using UnityEngine;

public class BurstFiringMode : IFiringModeStrategy
{
    private const float TimeBeforeReleasedBullet = 0.15f;

    private int _bulletReleasedCount = 3;

    public FiringMode FiringMode => FiringMode.BurstFireMode;

    public IEnumerator FiringWeaponJob(IWeapon weapon)
    {
        if (weapon == null)
        {
            Debug.LogWarning("Weapon not founded in BurstFiringMode!");

            weapon.StopShoot();

            yield break;
        }

        for (int i = 0; i < _bulletReleasedCount; i++)
        {
            if (weapon.IsCanFiring == false)
                break;

            weapon.StartShoot();

            yield return new WaitForSeconds(TimeBeforeReleasedBullet);
        }

        yield return new WaitForSeconds(weapon.DelayBeforeFiring);

        weapon.StopShoot();
    }
}
