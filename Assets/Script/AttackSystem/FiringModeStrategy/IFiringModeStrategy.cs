using System.Collections;

public interface IFiringModeStrategy
{
    FiringMode FiringMode { get; }

    IEnumerator FiringWeaponJob(IWeapon weapon);
}
