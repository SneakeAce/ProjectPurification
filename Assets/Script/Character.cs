using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Collider _collider;

    public Rigidbody Rigidbody { get => _rigidbody; }
    public Collider Collider { get => _collider; }
}
