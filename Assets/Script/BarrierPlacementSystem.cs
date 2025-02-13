using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class BarrierPlacementSystem : ObjectPlacementSystem
{
    protected ObjectPool<PlaceableObject> _poolObject;
    private Material _phantomObjectMaterial;

    public CreatedPoolBarriersSystem PoolBarrierSystem => _createdPools.PoolBarrierSystem;

    public override void Initialization(Character character)
    {
        _character = character;
        _playerInput = _character.PlayerInput;

        _placingJob = false;
        _canShowPhantomObject = true;
        _poolObjectSelected = false;

        _playerInput.PlacementObjectMode.TogglePlacementMode.performed -= OnTogglePlacementMode;
        _playerInput.PlacementObjectMode.DeactivatePlacementMode.performed -= OnDeactivatePlacementMode;

        _playerInput.PlacementObjectMode.TogglePlacementMode.performed += OnTogglePlacementMode;
        _playerInput.PlacementObjectMode.DeactivatePlacementMode.performed += OnDeactivatePlacementMode;
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
                _playerInput.PlacementTurretMode.Disable();

                // вызов UI в игре
                _playerInput.PlacementObjectMode.ChooseTypeOfBarrirer.performed -= OnChooseTypePlacingObject;

                _playerInput.PlacementObjectMode.ChooseTypeOfBarrirer.performed += OnChooseTypePlacingObject;

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
                    int selectedBarrierIndex = keyNumber - stepBackInList;

                    if (Enum.IsDefined(typeof(BarriersType), selectedBarrierIndex))
                    {

                        BarriersType selectedType = (BarriersType)selectedBarrierIndex;

                        if (PoolBarrierSystem.PoolDictionary.TryGetValue(selectedType, out ObjectPool<PlaceableObject> poolSelected))
                        {
                            _poolObject = poolSelected;

                            _currentPhantomObject = SelectedPhantomBarrier(selectedBarrierIndex);

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

                    Vector2 mouseScroll = _playerInput.PlacementObjectMode.RotatingObject.ReadValue<Vector2>();

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

        _phantomObjectMaterial = _instancePhantomObject.GetComponent<MeshRenderer>().material;

        _instancePhantomObject.transform.position = PlacingPosition();

        if (_instancePhantomObject.transform.position == Vector3.zero)
            return;

        if (_phantomObjectMaterial != null && CanPlacedObject(_instancePhantomObject.transform))
        {
            _phantomObjectMaterial.color = _colorBeingPlacedObject;

            _objectCanBePlaced = true;
        }
        else if (_phantomObjectMaterial != null && CanPlacedObject(_instancePhantomObject.transform) == false)
        {
            _phantomObjectMaterial.color = _colorNotBeingPlacedObject;

            _objectCanBePlaced = false;
        }
    }

    public override void PlaceObject()
    {
        PlaceableObject newObject = _poolObject.GetPoolObject();

        newObject.transform.SetParent(null);
        newObject.transform.position = _instancePhantomObject.transform.position;
        newObject.transform.rotation = _instancePhantomObject.transform.rotation;

        _phantomObjectMaterial = null;

        ResetVariables();
    }
}
