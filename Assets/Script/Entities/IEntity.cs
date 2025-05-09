using UnityEngine;

public interface IEntity
{
    Transform Transform { get; }
    Animator Animator { get; }
    Rigidbody Rigidbody { get; }
    Collider Collider { get; }
    IEntityHealth EntityHealth { get; }
}
