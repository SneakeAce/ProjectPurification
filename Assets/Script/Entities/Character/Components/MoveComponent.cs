using UnityEngine;

public class MoveComponent
{
    private const float MinSpeed = 0.0f;
    private const float MaxSpeed = 1.0f;
    private const float MinValueDirection = 0.1f;
    private const int MinYCoordinate = 0;

    private float _speed;
    private LayerMask _includeLayer;

    private Character _character;
    private PlayerInput _playerInput;

    private Vector3 _moveDirection;
    private Vector3 _targetPoint;

    private bool _isCanWork = false;

    public MoveComponent(Character character, PlayerConfig config)
    {
        _character = character;
        _playerInput = _character.PlayerInput;

        _speed = config.UniqueCharacterisitcs.MoveSpeed;
        _includeLayer = config.UniqueCharacterisitcs.IncludeLayerForMovement;

        _isCanWork = true;
    }

    public void Update()
    {
        if (_isCanWork == false)
            return;

        RotateToTarget();
    }

    public void FixedUpdate()
    {
        if (_isCanWork == false)
            return;

        Move();
    }

    private void RotateToTarget()
    {
        float mouseZPosition = 0;

        Vector2 inputMousePosition = _playerInput.MousePosition.MousePosition.ReadValue<Vector2>();
        Vector3 mousePosition = new Vector3(inputMousePosition.x, inputMousePosition.y, mouseZPosition);

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, _includeLayer))
        {
            _targetPoint = hitInfo.point;
            _targetPoint.y = MinYCoordinate;

            Vector3 direction = _targetPoint - _character.transform.position;
            direction.y = MinYCoordinate;

            if (direction.sqrMagnitude > MinValueDirection)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                _character.transform.rotation = targetRotation;
            }
        }
    }

    private void Move()
    {
        Vector2 directionMove = _playerInput.PlayerMovement.PlayerMovement.ReadValue<Vector2>();

        _moveDirection = new Vector3(directionMove.x, MinYCoordinate, directionMove.y);
        _moveDirection.y = MinYCoordinate;

        if (_moveDirection.sqrMagnitude > MinValueDirection)
        {
            _moveDirection = _character.transform.TransformDirection(_moveDirection.normalized);
            _moveDirection *= _speed;

            _character.Rigidbody.velocity = new Vector3(_moveDirection.x, MinYCoordinate, _moveDirection.z);
        }
        else
        {
            float newSpeed = 0;
            _moveDirection *= newSpeed;
            _character.Rigidbody.velocity = new Vector3(_moveDirection.x, MinYCoordinate, _moveDirection.z);
        }

        float moveSpeed = Mathf.Clamp(_moveDirection.magnitude, MinSpeed, MaxSpeed);
        _character.Animator.SetFloat("MoveSpeed", moveSpeed);
        _character.Animator.SetFloat("MoveX", directionMove.x);
        _character.Animator.SetFloat("MoveZ", directionMove.y);
    }

}
