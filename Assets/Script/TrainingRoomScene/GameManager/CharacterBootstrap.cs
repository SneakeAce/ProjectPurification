using UnityEngine;

public class CharacterBootstrap : MonoBehaviour
{
    [Header("Character Components")]
    [SerializeField] private Character _character;
    [SerializeField] private MoveComponent _moveComponent;
    [SerializeField] private CharacterHealth _health;

    [Header("Weapon")]
    [SerializeField] private Weapon _weapon;
    [SerializeField] private ObjectPlacementSystem _placementController;

    public void Initialization()
    {
        _character.Initialization();
        _moveComponent.Initialize(_character);
        _weapon.Initialize(_character);
        _health.Initialize();

        _placementController.Initialization(_character);
    }
}
