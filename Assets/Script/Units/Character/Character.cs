using UnityEngine;
using Zenject;

public class Character : MonoBehaviour, IUnit
{
    private PlayerInput _playerInput;
    private PlayerConfig _playerConfig;
    private CharacterHealth _health;
    private MoveComponent _moveComponent;

    private Rigidbody _rigidbody;
    private Collider _collider;
    private Animator _animator;

    private WeaponHolder _weaponHolder;

    [Inject]
    private void Construct(PlayerInput playerInput, PlayerConfig playerConfig, CharacterHealth health, MoveComponent moveComponent)
    {
        _playerInput = playerInput;
        _playerInput.Enable();

        _playerConfig = playerConfig;

        _health = health;
        _moveComponent = moveComponent;

        GetWeaponHolder();

        Initialization();
    }

    public PlayerInput PlayerInput => _playerInput;
    public Rigidbody Rigidbody => _rigidbody;
    public Collider Collider => _collider;
    public Animator Animator => _animator;
    public PlayerConfig PlayerConfig => _playerConfig;
    public WeaponHolder WeaponHolder => _weaponHolder;

    private void GetWeaponHolder()
    {
        _weaponHolder = GetComponentInChildren<WeaponHolder>();
    }

    private void Initialization()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _animator = GetComponent<Animator>();

        _health.Initialization(this);
        _health.OnDead += DestroyCharacter;

        _moveComponent.Initialization(this);
    }

    private void Update()
    {
        RotateToMousePosition();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _moveComponent.FixedUpdate();
    }

    private void RotateToMousePosition()
    {
        _moveComponent.Update();
    }

    private void DestroyCharacter(Character character)
    {
        _health.OnDead -= DestroyCharacter;

        Destroy(character.gameObject);
    }
}
