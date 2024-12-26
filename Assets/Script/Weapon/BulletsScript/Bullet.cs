using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private MoveBullet _moveBullet;

    public Rigidbody Rigidbody { get => _rigidbody; }

    public void InitializeBullet(Vector3 startPoint, float distanceFlying)
    {
        _moveBullet.Initialize(this, startPoint, distanceFlying);
    }

    // Код, который отвечает за столкновение с препятствиями.

}
