using System;

[Flags]
public enum EnemyType
{
    None = 0,
    NormalZombie = 1 << 0,
    BigZombie = 1 << 1,
    SpittingZombie = 1 << 2,
}
