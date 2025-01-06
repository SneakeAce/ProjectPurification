using UnityEngine;

public abstract class MoveComponent : MonoBehaviour
{
    [SerializeField] protected float _speed;

    public abstract void Move(Unit character);
    public abstract void RotateToTarget(Unit character);
}
