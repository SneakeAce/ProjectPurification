using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [Header("Unity Components")]
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Collider _collider;
    [SerializeField] private Animator _animator;

    [Header("Self-made Components")]
    [SerializeField] private Health _health;

    public Rigidbody Rigidbody { get => _rigidbody; }
    public Collider Collider { get => _collider; }
    public Animator Animator { get => _animator; }
    public Health Health { get => _health; }
}
