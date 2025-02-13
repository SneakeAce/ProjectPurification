using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class ObjectPlacementSystem : MonoBehaviour
{
    [SerializeField] protected PoolCreator _createdPools;
    [SerializeField] protected List<GameObject> _phantomObjectsPrefab;

    [SerializeField] protected LayerMask _objectsIncludeLayer;

    [SerializeField] protected Color _colorBeingPlacedObject;
    [SerializeField] protected Color _colorNotBeingPlacedObject;

    [SerializeField] protected float _radiusPlacing;
    [SerializeField] protected float _objectRotationSpeed;
    
    protected GameObject _instancePhantomObject;
    protected GameObject _currentPhantomObject;

    protected Character _character;
    protected PlayerInput _playerInput;
    protected Coroutine _placementModeCoroutine;

    protected bool _objectCanBePlaced;
    protected bool _placingJob;
    protected bool _canShowPhantomObject;
    protected bool _poolObjectSelected;

    public abstract void OnTogglePlacementMode(InputAction.CallbackContext context);

    public abstract void OnChooseTypePlacingObject(InputAction.CallbackContext context);

    public abstract void OnDeactivatePlacementMode(InputAction.CallbackContext context);

    public abstract void Initialization(Character character);
    public abstract IEnumerator PlacementModeJob();
    public abstract void ShowObjectPosition();
    public abstract void PlaceObject();

    public void StartWork()
    {
        if (_placementModeCoroutine != null)
            ResetVariables();
        
        _placementModeCoroutine = StartCoroutine(PlacementModeJob());
    }

    protected GameObject SelectedPhantomBarrier(int index)
    {
        return _phantomObjectsPrefab[index];
    }

    protected void ResetVariables()
    {
        _playerInput.PlayerShooting.Enable();
        _playerInput.PlacementTurretMode.Enable();
        _playerInput.PlacementObjectMode.Enable();
        _playerInput.UI.Enable();

        _playerInput.PlacementObjectMode.ChooseTypeOfBarrirer.performed -= OnChooseTypePlacingObject; 
        _playerInput.PlacementTurretMode.ChooseTypeTurret.performed -= OnChooseTypePlacingObject;


        _canShowPhantomObject = true;
        _placingJob = false;
        _objectCanBePlaced = false;
        _poolObjectSelected = false;

        if (_instancePhantomObject != null)
        {
            Destroy(_instancePhantomObject);
            _instancePhantomObject = null;
        }
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
        
        Collider[] obstacleAround = Physics.OverlapBox(objectTransform.position, halfSizeBox, objectTransform.rotation, _objectsIncludeLayer);

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
