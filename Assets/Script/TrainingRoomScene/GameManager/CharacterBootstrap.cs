using UnityEngine;
using Zenject;

public class CharacterBootstrap : MonoBehaviour
{
    private Character _character;

    [SerializeField] private ObjectPlacementSystem _barrierPlacementController;
    [SerializeField] private ObjectPlacementSystem _turretPlacementController;

    [Inject]
    private void Construct(Character character)
    {
        _character = character;
    }

    public void Initialization()
    {
        _character.Initialization();

        _barrierPlacementController.Initialization(_character);
        _turretPlacementController.Initialization(_character);
    }
}
