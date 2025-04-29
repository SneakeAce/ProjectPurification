using System;

[Flags]
public enum EnemyType
{
    None = 0 << 0,
    NormalZombie = 1 << 1,
    BigZombie = 1 << 2,
    SpittingZombie = 1 << 4,
}
