using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BarrierPlacementSystem : ObjectPlacementSystem
{
    private CreatedPoolBarriersSystem _poolBarriersSystem;

    protected ObjectPool<PlaceableObject> _poolObject;

    private Material _phantomObjectMaterial;

    public BarrierPlacementSystem(BarrierPlacementSystemConfig config, Character character, CreatedPoolBarriersSystem poolBarriersSystem) : base(config, character)
    {
        Debug.Log("BarrierPlacementSystem Construct");

        _modeNameInPlayerInput = config.ModeNameInPlayerInput;

        _poolBarriersSystem = poolBarriersSystem;
    }

    public override void ChooseTypePlacingObject(InputAction.CallbackContext context)
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

                        if (_poolBarriersSystem.PoolDictionary.TryGetValue(selectedType, out ObjectPool<PlaceableObject> poolSelected))
                        {
                            _poolObject = poolSelected;

                            _currentPhantomObject = SelectedPhantomObject(selectedBarrierIndex);

                            _poolObjectSelected = true;
                        }
                    }
                }

            }
        }
    }

    public override void WorkPlacementMode()
    {
        if (_poolObjectSelected && _placingJob && _poolObject != null)
        {
            if (_canShowPhantomObject)
            {
                ShowObjectPosition();

                Vector2 mouseScroll = _playerInput.PlacementBarrierMode.RotatingObject.ReadValue<Vector2>();

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
    }

    public override void ShowObjectPosition()
    {
        if (_instancePhantomObject == null)
        {
            CreateObject();

            _instancePhantomObject.transform.position = _character.transform.position;
            _instancePhantomObject.transform.rotation = Quaternion.identity;
        }

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
