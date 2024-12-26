using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private MoveBullet _moveBullet;

    public Rigidbody Rigidbody { get => _rigidbody; }

    public void InitializeBullet(Transform spawnPointTransform, Vector3 directionToMousePosition)
    {
        //_moveBullet.Initialize(spawnPointTransform, directionToMousePosition, this);
    }

    // Код, который отвечает за столкновение с препятствиями.

}
