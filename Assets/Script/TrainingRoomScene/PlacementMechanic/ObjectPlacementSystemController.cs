using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.VirtualTexturing;
using Zenject;

public class ObjectPlacementSystemsController : MonoBehaviour
{
    private PlayerInput _playerInput;

    private Dictionary<string, IPlacementSystem> _placementSystems = new Dictionary<string, IPlacementSystem>();
    private IPlacementSystem _currentPlacementSystem;

    private Coroutine _workPlacementSystemCoroutine;

    private string _barrierPlacementModeName;
    private string _turretPlacementModeName;

    [Inject]
    private void Construct(PlayerInput playerInput, List<IPlacementSystem> placementSystems)
    {
        // Переделать так, чтобы в аргументах был List<IPlacmenetSystem>(), а потом из него извлекать нужную мне систему.
        _playerInput = playerInput;

        foreach(IPlacementSystem system in placementSystems)
        {
            _placementSystems.Add(system.ModeNameInPlayerInput, system);
        }

        foreach (var key in _placementSystems)
        {
            Debug.Log("dictionary key = " + key.Key + " / dictionary value = " + key.Value);
        }

        foreach(var dicitonaryKey in _placementSystems)
        {
            if (dicitonaryKey.Value is BarrierPlacementSystem)
            {
                _barrierPlacementModeName = dicitonaryKey.Key;
            }
            else if (dicitonaryKey.Value is TurretPlacementSystem)
            {
                _turretPlacementModeName = dicitonaryKey.Key;
            }
        }

        StartSetupInputActions();
    }

    private void StartSetupInputActions()
    {
        Debug.Log("PlacementController / StartSetupInputActions");

        _playerInput.PlacementBarrierMode.TogglePlacementMode.performed -= OnTogglePlacementObjectMode;
        _playerInput.PlacementBarrierMode.ChooseTypeOfBarrirer.performed -= OnChooseTypeOfPlacementObject;
        _playerInput.PlacementBarrierMode.DeactivatePlacementMode.performed -= OnDeactivatePlacementMode;

        _playerInput.PlacementTurretMode.TogglePlacementMode.performed -= OnTogglePlacementObjectMode;
        _playerInput.PlacementTurretMode.ChooseTypeTurret.performed -= OnChooseTypeOfPlacementObject;
        _playerInput.PlacementTurretMode.DeactivateMode.performed -= OnDeactivatePlacementMode;        
        
        _playerInput.PlacementBarrierMode.TogglePlacementMode.performed += OnTogglePlacementObjectMode;
        _playerInput.PlacementBarrierMode.ChooseTypeOfBarrirer.performed += OnChooseTypeOfPlacementObject;
        _playerInput.PlacementBarrierMode.DeactivatePlacementMode.performed += OnDeactivatePlacementMode;

        _playerInput.PlacementTurretMode.TogglePlacementMode.performed += OnTogglePlacementObjectMode;
        _playerInput.PlacementTurretMode.ChooseTypeTurret.performed += OnChooseTypeOfPlacementObject;
        _playerInput.PlacementTurretMode.DeactivateMode.performed += OnDeactivatePlacementMode;
    }

    private void OnTogglePlacementObjectMode(InputAction.CallbackContext context)
    {
        if (context.action.actionMap.name == _barrierPlacementModeName)
        {
            SetCurrentPlacementSystem(context, _barrierPlacementModeName);

            _playerInput.PlacementTurretMode.Disable();
        }
        else if (context.action.actionMap.name == _turretPlacementModeName)
        {
            SetCurrentPlacementSystem(context, _turretPlacementModeName);

            _playerInput.PlacementBarrierMode.Disable();
        }

        _playerInput.UI.Disable();
        _playerInput.PlayerShooting.Disable();
    }

    private void SetCurrentPlacementSystem(InputAction.CallbackContext context, string namePlacementSystem)
    {
        if (_placementSystems.TryGetValue(namePlacementSystem, out var newPlacementSystem))
        {
            if (_currentPlacementSystem == newPlacementSystem)
                return;

            if (_currentPlacementSystem == null)
                _currentPlacementSystem = newPlacementSystem;

            _currentPlacementSystem?.ExitMode();

            _currentPlacementSystem.CreatePhantomObject -= OnCreatePhantomObject;
            _currentPlacementSystem.CreatePhantomObject -= OnDestroyPhantomObject;
            _currentPlacementSystem.StopWork -= ResetInputActions;

            _currentPlacementSystem = newPlacementSystem;

            _currentPlacementSystem.CreatePhantomObject += OnCreatePhantomObject;
            _currentPlacementSystem.CreatePhantomObject += OnDestroyPhantomObject;
            _currentPlacementSystem.StopWork += ResetInputActions;

            _currentPlacementSystem.EnterMode(context);

            StartWorkPlacementSystemCoroutine();
        }
    }

    private void OnChooseTypeOfPlacementObject(InputAction.CallbackContext context)
    {
        if (context.action.actionMap.name == _barrierPlacementModeName)
            ChooseTypeOfPlacementObject(context, _barrierPlacementModeName);

        else if (context.action.actionMap.name == _turretPlacementModeName)
            ChooseTypeOfPlacementObject(context, _turretPlacementModeName);
    }

    private void ChooseTypeOfPlacementObject(InputAction.CallbackContext context, string namePlacementSystem)
    {
        if (_placementSystems.TryGetValue(namePlacementSystem, out var currentPlacementSystem))
        {
            if (_currentPlacementSystem != currentPlacementSystem)
                return;

            _currentPlacementSystem.ChooseTypeOfPlacingObject(context);
        }
    }

    private void OnDeactivatePlacementMode(InputAction.CallbackContext context)
    {
        if (context.action.actionMap.name == _barrierPlacementModeName)
            DeactivatePlacementMode(context, _barrierPlacementModeName);

        else if (context.action.actionMap.name == _turretPlacementModeName)
            DeactivatePlacementMode(context, _turretPlacementModeName);
    }

    private void DeactivatePlacementMode(InputAction.CallbackContext context, string namePlacementSystem)
    {
        if (_placementSystems.TryGetValue(namePlacementSystem, out var currentPlacementSystem))
        {
            if (_currentPlacementSystem != currentPlacementSystem)
                return;

            _currentPlacementSystem.ExitMode();
        }
    }

    private void StartWorkPlacementSystemCoroutine()
    {
        if (_workPlacementSystemCoroutine != null)
        {
            StopCoroutine(_workPlacementSystemCoroutine);
            _workPlacementSystemCoroutine = null;
        }

        _workPlacementSystemCoroutine = StartCoroutine(WorkPlacementSystemJob());
    }

    private void StopWorkPlacementSystemCoroutine()
    {
        if (_workPlacementSystemCoroutine != null)
        {
            StopCoroutine(_workPlacementSystemCoroutine);
            _workPlacementSystemCoroutine = null;
        }
    }

    private IEnumerator WorkPlacementSystemJob()
    {
        while (_currentPlacementSystem.PlacingJob)
        {
            _currentPlacementSystem.Work();

            yield return null;
        }

        if (_workPlacementSystemCoroutine != null)
        {
            StopCoroutine(_workPlacementSystemCoroutine);
            _workPlacementSystemCoroutine = null;
        }
    }

    private void ResetInputActions()
    {
        StopWorkPlacementSystemCoroutine();

        _playerInput.UI.Enable();
        _playerInput.PlayerShooting.Enable();
        _playerInput.PlacementTurretMode.Enable();
        _playerInput.PlacementBarrierMode.Enable();

        _currentPlacementSystem.CreatePhantomObject -= OnCreatePhantomObject;
        _currentPlacementSystem.CreatePhantomObject -= OnDestroyPhantomObject;
        _currentPlacementSystem.StopWork -= ResetInputActions;
    }

    private void OnDestroyPhantomObject(GameObject phantomObject)
    {
        if (phantomObject != null)
            Destroy(phantomObject);
    }

    private void OnCreatePhantomObject(GameObject prefab)
    {
        Instantiate(prefab);
    }
}
