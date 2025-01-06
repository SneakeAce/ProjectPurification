using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Health _health;
    [SerializeField] private HealthBar _healthBar;

    private void Awake()
    {
        _weapon.Initialize(_character);
        _health.Initialize();
        _healthBar.Initialize();
    }
}
