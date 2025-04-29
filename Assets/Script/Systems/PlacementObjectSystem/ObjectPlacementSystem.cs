using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class ObjectPlacementSystem : IPlacementSystem
{
    protected LayerMask _obstaclesLayers;

    protected Color _colorBeingPlacedObject;
    protected Color _colorNotBeingPlacedObject;

    protected List<GameObject> _phantomObjectsPrefab;
    protected GameObject _instancePhantomObject;
    protected GameObject _currentPhantomObject;

    protected Character _character;
    protected PlayerInput _playerInput;

    protected float _radiusPlacing;
    protected float _objectRotationSpeed;

    protected bool _objectCanBePlaced;
    protected bool _placingJob;
    protected bool _canShowPhantomObject;
    protected bool _poolObjectSelected;

    protected string _modeNameInPlayerInput;

    public bool PlacingJob { get => _placingJob; }

    public string ModeNameInPlayerInput => _modeNameInPlayerInput;

    public event Action StopWork;
    public event Func<GameObject, GameObject> CreatePhantomObject;
    public event Action<GameObject> DestroyPhantomObject;

    protected ObjectPlacementSystem(ObjectPlacementSystemConfig config, Character character)
    {
        _character = character;
        _playerInput = _character.PlayerInput;

        _obstaclesLayers = config.ObstaclesLayer;

        _colorBeingPlacedObject = config.ColorBeingPlacedObject;
        _colorNotBeingPlacedObject = config.ColorNotBeingPlacedObject;

        _phantomObjectsPrefab = config.PhantomObjectsPrefab;

        _radiusPlacing = config.RadiusPlacing;
        _objectRotationSpeed = config.RotationSpeedObject;
    }

    public abstract void ChooseTypePlacingObject(InputAction.CallbackContext context);
    public abstract void WorkPlacementMode();
    public abstract void ShowObjectPosition();
    public abstract void PlaceObject();

    public void EnterMode(InputAction.CallbackContext context)
    {
        _placingJob = !_placingJob;

        if (_placingJob)
        {
            _canShowPhantomObject = true;

           // Debug.Log($"Called UI Placement System: {this}");
            // вызов UI в игре
        }
        else
        {
            ResetVariables();
        }
    }

    public void ChooseTypeOfPlacingObject(InputAction.CallbackContext context)
    {
        if (_placingJob)
            ChooseTypePlacingObject(context);
        else
            return;
    }

    public void ExitMode()
    {
        if (_placingJob)
            ResetVariables();
        else
            return;
    }

    public void Work()
    {
        WorkPlacementMode();
    }

    public GameObject CreateObject()
    {
        GameObject instance = CreatePhantomObject?.Invoke(_currentPhantomObject);

        return instance;
    }

    public void DestroyObject()
    {
        DestroyPhantomObject?.Invoke(_instancePhantomObject);
    }

    protected GameObject SelectedPhantomObject(int index)
    {
        return _phantomObjectsPrefab[index];
    }

    protected void ResetVariables()
    {
        _canShowPhantomObject = true;
        _placingJob = false;
        _objectCanBePlaced = false;
        _poolObjectSelected = false;

        if (_instancePhantomObject != null)
        {
            DestroyObject();
            _instancePhantomObject = null;
        }

        StopWork?.Invoke();
    }

    protected Quaternion RotationPlacedObject(GameObject objectPlaced, float scroll)
    {
        Quaternion targetRotation = objectPlaced.transform.rotation * Quaternion.Euler(0, scroll * _objectRotationSpeed, 0);

        return Quaternion.Lerp(objectPlaced.transform.rotation, targetRotation, _objectRotationSpeed * Time.deltaTime);
    }

    protected bool CanPlacedObject(Transform objectTransform)
    {
        float divider = 2f;

        Bounds boundsObj = objectTransform.gameObject.GetComponent<BoxCollider>().bounds;

        Vector3 halfSizeBox = new Vector3(boundsObj.size.x / divider, boundsObj.size.y / divider, boundsObj.size.z / divider);
        
        Collider[] obstacleAround = Physics.OverlapBox(objectTransform.position, halfSizeBox, objectTransform.rotation, _obstaclesLayers);

        if (obstacleAround.Length <= 0 && Vector3.Distance(_character.transform.position, objectTransform.position) <= _radiusPlacing)
        {
            return true;
        }

        return false;
    }

    protected Vector3 PlacingPosition()
    {
        Vector2 inputMousePosition = _playerInput.MousePosition.MousePosition.ReadValue<Vector2>();
        Vector3 mousePosition = new Vector3(inputMousePosition.x, inputMousePosition.y, 0f);

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            Vector3 point = hitInfo.point;
            point.y = 0;

            return point;
        }

        return Vector3.zero;
    }
}
