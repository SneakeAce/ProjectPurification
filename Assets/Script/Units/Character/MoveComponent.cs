using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    private const float MinSpeed = 0.0f;
    private const float MaxSpeed = 1.0f;

    [SerializeField] private float _speed;
    [SerializeField] private LayerMask _includeLayer;

    private Character _character;
    private Vector3 _moveDirection;
    private Vector3 _targetPoint;

    public void Initialize(Character character)
    {
        _character = character;
    }

    private void Update()
    {
        RotateToTarget();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void RotateToTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, _includeLayer))
        {
            _targetPoint = hitInfo.point;
            _targetPoint.y = 0;

            Vector3 direction = _targetPoint - _character.transform.position;
            direction.y = 0;

            if (direction.sqrMagnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                _character.transform.rotation = targetRotation;
            }
        }
    }

    private void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        _moveDirection = new Vector3(moveHorizontal, 0.0f, moveVertical);
        _moveDirection.y = 0;

        if (_moveDirection.sqrMagnitude > 0.1f)
        {
            Debug.Log("moveDirection.sqrMagnitude > 0.1");
            _moveDirection = _character.transform.TransformDirection(_moveDirection.normalized);
            _moveDirection *= _speed;

            _character.Rigidbody.velocity = new Vector3(_moveDirection.x, 0, _moveDirection.z);
        }
        else
        {
            Debug.Log("moveDirection.sqrMagnitude < 0.1");
            float newSpeed = 0;
            _moveDirection *= newSpeed;
            _character.Rigidbody.velocity = new Vector3(_moveDirection.x, 0, _moveDirection.z);
        }

        float moveSpeed = Mathf.Clamp(_moveDirection.magnitude, MinSpeed, MaxSpeed);
        _character.Animator.SetFloat("MoveSpeed", moveSpeed);
        _character.Animator.SetFloat("MoveX", moveHorizontal);
        _character.Animator.SetFloat("MoveZ", moveVertical);
    }

}
