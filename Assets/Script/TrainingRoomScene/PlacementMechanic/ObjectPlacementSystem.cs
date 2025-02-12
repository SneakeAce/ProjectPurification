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
    
    protected ObjectPool<PlaceableObject> _poolObject;
    protected GameObject _instancePhantomObject;
    protected GameObject _currentPhantomObject;

    protected Material _phantomObjectMaterial;
    protected Color _baseColorPhantomObject;

    protected Character _character;
    protected PlayerInput _playerInput;
    private Coroutine _placementModeCoroutine;

    protected bool _objectCanBePlaced;
    protected bool _placingJob;
    protected bool _canShowPhantomObject;
    protected bool _poolObjectSelected;

    public abstract void OnTogglePlacementMode(InputAction.CallbackContext context);

    public abstract void OnChooseTypePlacingObject(InputAction.CallbackContext context);

    public abstract void OnDeactivatePlacementMode(InputAction.CallbackContext context);

    public abstract void Initialization(Character character);

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
        StopCoroutine(_placementModeCoroutine);
        _placementModeCoroutine = null;

        _playerInput.PlayerShooting.Enable();
        _playerInput.UI.Enable();
        _playerInput.PlacementObjectMode.ChooseTypeOfBarrirer.performed -= OnChooseTypePlacingObject;

        _canShowPhantomObject = true;
        _placingJob = false;
        _objectCanBePlaced = false;
        _poolObjectSelected = false;

        _phantomObjectMaterial = null;

        if (_instancePhantomObject != null)
        {
            Destroy(_instancePhantomObject);
            _instancePhantomObject = null;
        }
    }

    private IEnumerator PlacementModeJob()
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
    }

    private void PlaceObject()
    {
        PlaceableObject newObject = _poolObject.GetPoolObject();

        newObject.transform.SetParent(null);
        newObject.transform.position = _instancePhantomObject.transform.position;
        newObject.transform.rotation = _instancePhantomObject.transform.rotation;

        _phantomObjectMaterial.color = _baseColorPhantomObject;

        ResetVariables();
    }

    private void ShowObjectPosition()
    {
        if (_instancePhantomObject == null)
            _instancePhantomObject = Instantiate(_currentPhantomObject, _character.transform.position, Quaternion.identity);

        if (_phantomObjectMaterial == null)
        {
            _phantomObjectMaterial = _instancePhantomObject.GetComponent<MeshRenderer>().material;

            _baseColorPhantomObject = _phantomObjectMaterial.color;
        }

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

    private Quaternion RotationPlacedObject(GameObject objectPlaced, float scroll)
    {
        Quaternion targetRotation = objectPlaced.transform.rotation * Quaternion.Euler(0, scroll * _objectRotationSpeed, 0);

        return Quaternion.Lerp(objectPlaced.transform.rotation, targetRotation, _objectRotationSpeed * Time.deltaTime);
    }

    private bool CanPlacedObject(Transform objectTransform)
    {
        float divider = 2f;

        Bounds boundsObj = objectTransform.gameObject.GetComponent<BoxCollider>().bounds;

        Vector3 halfSizeBox = new Vector3(boundsObj.size.x / divider, boundsObj.size.y / divider, boundsObj.size.z / divider);
        
        Collider[] barriersAround = Physics.OverlapBox(objectTransform.position, halfSizeBox, objectTransform.rotation, _objectsIncludeLayer);

        if (barriersAround.Length <= 0 && Vector3.Distance(_character.transform.position, objectTransform.position) <= _radiusPlacing)
        {
            return true;
        }

        return false;
    }

    private Vector3 PlacingPosition()
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
