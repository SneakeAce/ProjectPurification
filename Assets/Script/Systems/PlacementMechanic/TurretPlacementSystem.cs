using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurretPlacementSystem : ObjectPlacementSystem
{    
    // ядекюрэ яхярелс нрякефхбюмхъ рейсыецн йнкхвеярбю пюяонкнфеммшу назейрнб ндмнцн рхою мю яжеме.

    private CreatedPoolTurretsSystem _poolTurretsSystem;

    private ObjectPool<Turret> _poolObject;

    private MeshRenderer[] _phantomObjectMesh;
    private List<Material> _phantomObjectMaterial = new List<Material>();

    public TurretPlacementSystem(TurretPlacementSystemConfig config, Character character, CreatedPoolTurretsSystem poolTurretsSystem) : base(config, character)
    {
        Debug.Log("TurretPlacementSystem Construct");

        _modeNameInPlayerInput = config.ModeNameInPlayerInput;

        _poolTurretsSystem = poolTurretsSystem;
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
        Debug.Log("TurretPlacementsSystem / WorkPlacementMode");
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
        Turret newObject = _poolObject.GetPoolObject();

        newObject.transform.SetParent(null);
        newObject.transform.position = _instancePhantomObject.transform.position;
        newObject.transform.rotation = _instancePhantomObject.transform.rotation;

        _phantomObjectMaterial.Clear();
        Array.Clear(_phantomObjectMesh, 0, _phantomObjectMesh.Length);

        ResetVariables();
    }
}
