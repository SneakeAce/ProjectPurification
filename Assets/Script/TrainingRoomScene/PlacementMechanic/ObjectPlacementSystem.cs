using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectPlacementSystem : MonoBehaviour
{
    [SerializeField] private CreatedPoolsBootstrap _createdPools;

    [Header("Properties Object")]
    [SerializeField] private List<GameObject> _phantomObjectsPrefab;

    [SerializeField] private LayerMask _objectsIncludeLayer;
    
    [SerializeField] private Color _colorBeingPlacedObject;
    [SerializeField] private Color _colorNotBeingPlacedObject;

    [Header("Values Properties")]
    [SerializeField] private float _radiusPlacing;
    [SerializeField] private float _objectRotationSpeed;
    
    private ObjectPool<PlaceableObject> _poolObject;

    private GameObject _instancePhantomObject;
    private GameObject _currentPhantomObject;

    private Material _phantomObjectMaterial;
    private Color _baseColorPhantomObject;

    private Character _character;
    private PlayerInput _playerInput;

    private bool _objectCanBePlaced;
    private bool _placingJob;
    private bool _canShowPhantomObject;
    private bool _poolObjectSelected;

    private CreatedPoolBarriersSystem PoolBarrierSystem => _createdPools.PoolBarrierSystem;

    public void Initialization(Character character)
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

    public void OnTogglePlacementMode(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _placingJob = !_placingJob;

            if (_placingJob)
            {
                _playerInput.UI.Disable();
                _playerInput.PlayerShooting.Disable();

                // вызов UI в игре
                _playerInput.PlacementObjectMode.ChooseTypeOfBarrirer.performed -= OnChooseTypeBarrier;

                _playerInput.PlacementObjectMode.ChooseTypeOfBarrirer.performed += OnChooseTypeBarrier;
            }
            else
            {
                ResetVariables();
            }
        }
    }

    public void OnChooseTypeBarrier(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (int.TryParse(context.control.name, out int keyNumber))
            {
                if (keyNumber >= 1)
                {
                    int stepBackInList = 1;
                    int selectedBarrierIndex = keyNumber - stepBackInList;

                    Debug.Log("OnChooseTypeBarrier / selectedBarrierIndex = " + selectedBarrierIndex);

                    if (Enum.IsDefined(typeof(BarrierType), selectedBarrierIndex))
                    {

                        BarrierType selectedType = (BarrierType)selectedBarrierIndex;
                        Debug.Log("OnChooseTypeBarrier / Enum IsDefined!!! / selectedType = " + selectedType);

                        if (PoolBarrierSystem.PoolDictionary.TryGetValue(selectedType, out ObjectPool<PlaceableObject> poolSelected))
                        { 
                            Debug.Log("OnChooseTypeBarrier / Getting Value!!!");

                            _poolObject = poolSelected;

                            _currentPhantomObject = SelectedPhantomBarrier(selectedBarrierIndex);

                            _poolObjectSelected = true;
                        }

                    }
                }

            }
        }
    }

    public void OnDeactivatePlacementMode(InputAction.CallbackContext context)
    {
        if (context.performed && _placingJob)
        {
            _placingJob = false;
            ResetVariables();
        }
    }

    private GameObject SelectedPhantomBarrier(int index)
    {
        return _phantomObjectsPrefab[index];
    }

    private void Update()
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
                    _instancePhantomObject.transform.rotation =  RotationPlacedObject(_instancePhantomObject, scroll);
                }

            }

            if (Input.GetMouseButtonDown(0) && _objectCanBePlaced)
            {
                _canShowPhantomObject = false;

                PlaceObject();
            }
                
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

    private void ResetVariables()
    {
        _playerInput.PlayerShooting.Enable();
        _playerInput.UI.Enable();
        _playerInput.PlacementObjectMode.ChooseTypeOfBarrirer.performed -= OnChooseTypeBarrier;

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
}
