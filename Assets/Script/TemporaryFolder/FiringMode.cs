using System;

[Flags]
public enum FiringMode
{
    AutomaticFireMode = 0 << 0,
    BurstFireMode = 1 << 1,
    SingleFireMode = 2 << 2,
}
