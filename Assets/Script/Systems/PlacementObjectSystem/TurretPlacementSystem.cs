using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurretPlacementSystem : ObjectPlacementSystem
{
    // ������� ������� ������������ �������� ���������� ������������� �������� ������ ���� �� �����.
    private TurretType _currentTurretType;

    private IFactory<Turret,TurretConfig, TurretType> _factory;
    private CreatedPoolTurretsSystem _poolTurretsSystem;

    private ObjectPool<Turret> _poolObject;

    private MeshRenderer[] _phantomObjectMesh;
    private List<Material> _phantomObjectMaterial = new List<Material>();

    public TurretPlacementSystem(TurretPlacementSystemConfig config, Character character, 
        CreatedPoolTurretsSystem poolTurretsSystem, IFactory<Turret, TurretConfig, TurretType> factory) : base(config, character)
    {
        _modeNameInPlayerInput = config.ModeNameInPlayerInput;

        _poolTurretsSystem = poolTurretsSystem;
        _factory = factory;
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
                    int selectedTurretIndex = keyNumber - stepBackInList;

                    if (Enum.IsDefined(typeof(TurretType), selectedTurretIndex))
                    {
                        TurretType selectedType = (TurretType)selectedTurretIndex;

                        if (_poolTurretsSystem.PoolDictionary.TryGetValue(selectedType, out ObjectPool<Turret> poolSelected))
                        {
                            _currentTurretType = selectedType;

                            _poolObject = poolSelected;

                            _currentPhantomObject = SelectedPhantomObject(selectedTurretIndex);

                            _poolObjectSelected = true;
                        }

                    }
                }

            }
        }
    }

    public override void WorkPlacementMode()
    {
        //Debug.Log("TurretPlacementsSystem / WorkPlacementMode");
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
    }

    public override void ShowObjectPosition()
    {
        if (_instancePhantomObject == null)
        {
            _instancePhantomObject = CreateObject();

            _instancePhantomObject.transform.position = _character.transform.position;
            _instancePhantomObject.transform.rotation = Quaternion.identity;
        }

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
        Vector3 spawnPosition = _instancePhantomObject.transform.position;
        Quaternion rotation = _instancePhantomObject.transform.rotation;

        Debug.Log($"Turret rotation = {rotation}");
            
        Turret newObject = _factory.Create(spawnPosition, _currentTurretType, rotation);

        _phantomObjectMaterial.Clear();
        Array.Clear(_phantomObjectMesh, 0, _phantomObjectMesh.Length);

        ResetVariables();
    }
}
