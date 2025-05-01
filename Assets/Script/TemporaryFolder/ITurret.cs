using UnityEngine;

public interface ITurret : IEntity
{
    AutomaticTurretWeapon TurretWeapon { get; }
    GameObject BodyTurret { get; }
}
