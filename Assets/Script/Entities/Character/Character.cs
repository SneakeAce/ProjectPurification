using UnityEngine;
using Zenject;

public class Character : MonoBehaviour, ICharacter
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
    private void Construct(PlayerInput playerInput, PlayerConfig playerConfig)
    {
        _playerInput = playerInput;
        _playerInput.Enable();

        _playerConfig = playerConfig;

        GetWeaponHolder();

        Initialization();
    }

    public Transform Transform => this.transform;
    public PlayerInput PlayerInput => _playerInput;
    public Rigidbody Rigidbody => _rigidbody;
    public Collider Collider => _collider;
    public Animator Animator => _animator;
    public WeaponHolder WeaponHolder => _weaponHolder;
    public CharacterHealth CharacterHealth => _health;

    private void GetWeaponHolder()
    {
        _weaponHolder = GetComponentInChildren<WeaponHolder>();
    }

    private void Initialization()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _animator = GetComponent<Animator>();

        _health = new CharacterHealth(this, _playerConfig);
        _health.OnDead += DestroyCharacter;

        _moveComponent = new MoveComponent(this, _playerConfig);
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

    private void OnDestroy()
    {
        _health.OnDead -= DestroyCharacter;
    }
}
