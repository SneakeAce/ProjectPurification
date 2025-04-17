public interface IWeapon
{
    float DelayBeforeFiring { get; }
    bool IsFiring { get; }
    bool IsCanFiring { get; }

    void StartShoot();
    void StopShoot();
}
