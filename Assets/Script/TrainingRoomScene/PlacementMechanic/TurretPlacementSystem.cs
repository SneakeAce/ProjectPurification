using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurretPlacementSystem : ObjectPlacementSystem
{
    private ObjectPool<Turret> _poolObject;

    private MeshRenderer[] _phantomObjectMesh;
    private List<Material> _phantomObjectMaterial = new List<Material>();

    public CreatedPoolTurretsSystem PoolBarrierSystem => _createdPools.PoolTurretsSystem;

    public override void Initialization(Character character)
    {
        _character = character;
        _playerInput = _character.PlayerInput;

        _placingJob = false;
        _canShowPhantomObject = true;
        _poolObjectSelected = false;

        _playerInput.PlacementTurretMode.TogglePlacementMode.performed -= OnTogglePlacementMode;
        _playerInput.PlacementTurretMode.DeactivateMode.performed -= OnDeactivatePlacementMode;
                     
        _playerInput.PlacementTurretMode.TogglePlacementMode.performed += OnTogglePlacementMode;
        _playerInput.PlacementTurretMode.DeactivateMode.performed += OnDeactivatePlacementMode;
    }

    public override void OnTogglePlacementMode(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _placingJob = !_placingJob;

            if (_placingJob)
            {
                _playerInput.UI.Disable();
                _playerInput.PlayerShooting.Disable();
                _playerInput.PlacementObjectMode.Disable();

                // вызов UI в игре
                _playerInput.PlacementTurretMode.ChooseTypeTurret.performed -= OnChooseTypePlacingObject;

                _playerInput.PlacementTurretMode.ChooseTypeTurret.performed += OnChooseTypePlacingObject;
            }
            else
            {
                ResetVariables();
            }
        }
    }

    public override void OnChooseTypePlacingObject(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (int.TryParse(context.control.name, out int keyNumber))
            {
                if (keyNumber >= 1)
                {
                    int stepBackInList = 1;
                    int selectedTurretIndex = keyNumber - stepBackInList;

                    if (Enum.IsDefined(typeof(TurretType), selectedTurretIndex))
                    {
                        TurretType selectedType = (TurretType)selectedTurretIndex;

                        if (PoolBarrierSystem.PoolDictionary.TryGetValue(selectedType, out ObjectPool<Turret> poolSelected))
                        {
                            _poolObject = poolSelected;

                            _currentPhantomObject = SelectedPhantomBarrier(selectedTurretIndex);

                            _poolObjectSelected = true;

                            StartWork();
                        }

                    }
                }

            }
        }
    }

    public override void OnDeactivatePlacementMode(InputAction.CallbackContext context)
    {
        if (context.performed && _placingJob)
        {
            _placingJob = false;
            ResetVariables();
        }
    }

    public override IEnumerator PlacementModeJob()
    {
        while (_placingJob)
        {
            if (_poolObjectSelected && _placingJob && _poolObject != null)
            {
                if (_canShowPhantomObject)
                {
                    ShowObjectPosition();

                    Vector2 mouseScroll = _playerInput.PlacementTurretMode.RotatingTurret.ReadValue<Vector2>();

                    float scroll = mouseScroll.y;

                    if (scroll != 0)
                    {
                        _instancePhantomObject.transform.rotation = RotationPlacedObject(_instancePhantomObject, scroll);
                    }

                }

                if (Input.GetMouseButtonDown(0) && _objectCanBePlaced)
                {
                    _canShowPhantomObject = false;

                    PlaceObject();
                }

            }

            yield return null;
        }

        StopCoroutine(_placementModeCoroutine);
        _placementModeCoroutine = null;
    }

    public override void ShowObjectPosition()
    {
        if (_instancePhantomObject == null)
            _instancePhantomObject = Instantiate(_currentPhantomObject, _character.transform.position, Quaternion.identity);

        _phantomObjectMesh = _instancePhantomObject.GetComponentsInChildren<MeshRenderer>();

        if (_phantomObjectMaterial.Count == 0)
        {
            for (int i = 0; i < _phantomObjectMesh.Length; i++)
            {
                _phantomObjectMaterial.Add(_phantomObjectMesh[i].material);
            }
        }

        _instancePhantomObject.transform.position = PlacingPosition();

        for (int i = 0; i < _phantomObjectMaterial.Count; i++)
        {
            if (_phantomObjectMaterial[i] != null && CanPlacedObject(_instancePhantomObject.transform))
            {
                _phantomObjectMaterial[i].color = _colorBeingPlacedObject;

                _objectCanBePlaced = true;
            }
            else if (_phantomObjectMaterial[i] != null && CanPlacedObject(_instancePhantomObject.transform) == false)
            {
                _phantomObjectMaterial[i].color = _colorNotBeingPlacedObject;

                _objectCanBePlaced = false;
            }
        }
    }

    public override void PlaceObject()
    {
        Turret newObject = _poolObject.GetPoolObject();

        newObject.transform.SetParent(null);
        newObject.transform.position = _instancePhantomObject.transform.position;
        newObject.transform.rotation = _instancePhantomObject.transform.rotation;

        _phantomObjectMaterial.Clear();
        Array.Clear(_phantomObjectMesh, 0, _phantomObjectMesh.Length);

        ResetVariables();
    }
}
