using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [Header("Character Components")]
    [SerializeField] private Character _character;
    [SerializeField] private MoveComponent _moveComponent;
    [SerializeField] private CharacterHealth _health;

    [Header("Weapon")]
    [SerializeField] private Weapon _weapon;

    [Header("Interface and UI")]
    [SerializeField] private HealthBar _healthBar;

    private void Awake()
    {
        _moveComponent.Initialize(_character);
        _weapon.Initialize(_character);
        _health.Initialize();
        _healthBar.Initialize();
    }
}
