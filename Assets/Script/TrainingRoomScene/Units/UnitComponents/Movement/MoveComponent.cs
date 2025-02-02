using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    private const float MinSpeed = 0.0f;
    private const float MaxSpeed = 1.0f;

    [SerializeField] private float _speed;
    [SerializeField] private LayerMask _includeLayer;

    private Character _character;
    private PlayerInput _playerInput;

    private Vector3 _moveDirection;
    private Vector3 _targetPoint;

    private bool _isCanWork = false;

    public void Initialize(Character character)
    {
        _character = character;
        _playerInput = _character.PlayerInput;

        _isCanWork = true;
    }

    private void Update()
    {
        if (_isCanWork == false)
            return;

        RotateToTarget();
    }

    private void FixedUpdate()
    {
        if (_isCanWork == false)
            return;

        Move();
    }

    private void RotateToTarget()
    {
        Vector2 inputMousePosition = _playerInput.MousePosition.MousePosition.ReadValue<Vector2>();
        Vector3 mousePosition = new Vector3(inputMousePosition.x, inputMousePosition.y, 0f);

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

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
        Vector2 directionMove = _playerInput.PlayerMovement.PlayerMovement.ReadValue<Vector2>();

        _moveDirection = new Vector3(directionMove.x, 0.0f, directionMove.y);
        _moveDirection.y = 0;

        if (_moveDirection.sqrMagnitude > 0.1f)
        {
            _moveDirection = _character.transform.TransformDirection(_moveDirection.normalized);
            _moveDirection *= _speed;

            _character.Rigidbody.velocity = new Vector3(_moveDirection.x, 0, _moveDirection.z);
        }
        else
        {
            float newSpeed = 0;
            _moveDirection *= newSpeed;
            _character.Rigidbody.velocity = new Vector3(_moveDirection.x, 0, _moveDirection.z);
        }

        float moveSpeed = Mathf.Clamp(_moveDirection.magnitude, MinSpeed, MaxSpeed);
        _character.Animator.SetFloat("MoveSpeed", moveSpeed);
        _character.Animator.SetFloat("MoveX", directionMove.x);
        _character.Animator.SetFloat("MoveZ", directionMove.y);
    }

}
