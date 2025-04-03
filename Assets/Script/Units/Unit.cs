using UnityEngine;

public interface IUnit
{
    public Rigidbody Rigidbody { get; }
    public Collider Collider { get; }
    public Animator Animator { get; }
}
