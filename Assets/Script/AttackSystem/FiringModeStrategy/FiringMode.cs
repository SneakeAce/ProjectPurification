using System;

[Flags]
public enum FiringMode
{
    None = 0 << 0,
    AutomaticFireMode = 1 << 1,
    BurstFireMode = 1 << 2,
    SingleFireMode = 1 << 4,
}
