using DG.Tweening;
using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    private const float MinSpeed = 0.0f;
    private const float MaxSpeed = 1.0f;

    [SerializeField] private float _speed;

    private Character _character;

    public void Initialize(Character character)
    {
        _character = character;
    }

    private void Update()
    {
        Move(_character);

        RotateToTarget(_character);
    }

    public void RotateToTarget(Character character)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            Vector3 targetPoint = hitInfo.point;
            targetPoint.y = 0;

            Vector3 direction = this.transform.position - targetPoint;

            if (direction.sqrMagnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(-direction);
                _character.transform.rotation = targetRotation;
            }
        }
    }

    public void Move(Character character)
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVecrtical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveHorizontal, 0.0f, moveVecrtical);
        moveDirection.y = 0;

        float moveSpeed = Mathf.Clamp(moveDirection.magnitude, MinSpeed, MaxSpeed);

        Vector3 velocity = moveDirection * _speed;
        velocity.y = 0;
        _character.Rigidbody.velocity = velocity;

        character.Animator.SetFloat("MoveSpeed", moveSpeed);
        character.Animator.SetFloat("MoveX", moveHorizontal);
        character.Animator.SetFloat("MoveZ", moveVecrtical);
    }
}
