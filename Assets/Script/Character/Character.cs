using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Collider _collider;
    [SerializeField] private Animator _animator;

    private float _time = 30f;

    public Rigidbody Rigidbody { get => _rigidbody; }
    public Collider Collider { get => _collider; }
    public Animator Animator { get => _animator; }

    private void Update()
    {
        if (_time > 0)
        {
            _time -= Time.deltaTime;
        }
        else
        {
            ActiveSecondIdleAnimation();
        }
    }

    private void ActiveSecondIdleAnimation()
    {
        _time = 35f;

        _animator.SetTrigger("UseSecondIdleAnimation");
    }
}
