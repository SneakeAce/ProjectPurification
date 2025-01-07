using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    private const float MinSpeed = 0.0f;
    private const float MaxSpeed = 1.0f;

    [SerializeField] private float _speed;

    private Unit _thisUnit;

    public void Initialize(Unit unit)
    {
        _thisUnit = unit;
    }

    private void Update()
    {
        Move(_thisUnit);

        RotateToTarget(_thisUnit);
    }

    public void RotateToTarget(Unit character)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            Vector3 targetPoint = hitInfo.point;
            targetPoint.y = 0;

            character.transform.LookAt(targetPoint);
        }
    }

    public void Move(Unit character)
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVecrtical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveHorizontal, 0.0f, moveVecrtical);
        moveDirection.y = 0;

        float moveSpeed = Mathf.Clamp(moveDirection.magnitude, MinSpeed, MaxSpeed);

        character.transform.Translate(moveDirection * _speed * Time.deltaTime);

        character.Animator.SetFloat("MoveSpeed", moveSpeed);
        character.Animator.SetFloat("MoveX", moveHorizontal);
        character.Animator.SetFloat("MoveZ", moveVecrtical);
    }
}
