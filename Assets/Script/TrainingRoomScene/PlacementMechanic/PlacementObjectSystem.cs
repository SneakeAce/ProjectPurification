using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlacementObjectSystem : MonoBehaviour
{
    [Header("Properties Object")]
    [SerializeField] private GameObject _placedObjectPrefab;
    [SerializeField] private GameObject _phantomObjectPrefab;
    [SerializeField] private Color _colorBeingPlacedObject;
    [SerializeField] private Color _colorNotBeingPlacedObject;

    [Header("Values Properties")]
    [SerializeField] private float _radiusPlacing;
    [SerializeField] private float _objectRotationSpeed;

    private GameObject _instancePhantomObject;

    private Material _phantomObjectMaterial;
    private Color _baseColorPhantomObject;

    private Character _character;
    private PlayerInput _playerInput;

    private bool _objectCanBePlaced;
    private bool _placingJob;
    private bool _canShowPhantomObject;
    private bool _canDisableMode;

    public void Initialization(Character character)
    {
        _character = character; 
        _playerInput = _character.PlayerInput;

        _placingJob = false;
        _canShowPhantomObject = true;
        _canDisableMode = false;

        _playerInput.UI.Disable();

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
                // вызов UI в игре
            }
            else
            {
                ResetVariables();
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

    private void Update()
    {
        if (_placingJob && _placedObjectPrefab != null)
        {
            if (_canShowPhantomObject)
            {
                ShowObjectPosition();

                Vector2 mouseScroll = _playerInput.PlacementObjectMode.RotatingObject.ReadValue<Vector2>();

                float scroll = mouseScroll.y;

                if (scroll != 0)
                {
                    RotationPlacedObject(_instancePhantomObject, scroll);
                }

            }

            if (Input.GetMouseButtonDown(0) && _objectCanBePlaced)
            {
                _canShowPhantomObject = false;

                PlaceObject(_instancePhantomObject);
            }
                
        }
    }

    private void PlaceObject(GameObject placedObject)
    {
        GameObject objectPlacing = Instantiate(_placedObjectPrefab, placedObject.transform.position, placedObject.transform.rotation);

        _phantomObjectMaterial.color = _baseColorPhantomObject;

        ResetVariables();
    }

    private void ShowObjectPosition()
    {
        if (_instancePhantomObject == null)
            _instancePhantomObject = Instantiate(_phantomObjectPrefab, _character.transform.position, Quaternion.identity);

        if (_phantomObjectMaterial == null)
        {
            _phantomObjectMaterial = _instancePhantomObject.GetComponent<MeshRenderer>().material;

            _baseColorPhantomObject = _phantomObjectMaterial.color;
        }

        _instancePhantomObject.transform.position = PlacingPosition();

        if (_instancePhantomObject.transform.position == Vector3.zero)
            return;

        if (_phantomObjectMaterial != null && CanPlaced(_instancePhantomObject.transform.position))
        {
            _phantomObjectMaterial.color = _colorBeingPlacedObject;

            _objectCanBePlaced = true;
        }
        else if (_phantomObjectMaterial != null && CanPlaced(_instancePhantomObject.transform.position) == false)
        {
            _phantomObjectMaterial.color = _colorNotBeingPlacedObject;

            _objectCanBePlaced = false;
        }
    }

    private void RotationPlacedObject(GameObject objectPlaced, float scroll)
    {
        Quaternion targetRotation = objectPlaced.transform.rotation * Quaternion.Euler(0, scroll * _objectRotationSpeed, 0);

        objectPlaced.transform.rotation = Quaternion.Lerp(objectPlaced.transform.rotation, targetRotation, _objectRotationSpeed * Time.deltaTime);
    }

    private bool CanPlaced(Vector3 objectPosition)
    {
        if (Vector3.Distance(_character.transform.position, objectPosition) > _radiusPlacing)
            return false;

        return true;
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
        _playerInput.UI.Enable();

        _canShowPhantomObject = true;
        _placingJob = false;
        _objectCanBePlaced = false;

        _phantomObjectMaterial = null;

        if (_instancePhantomObject != null)
        {
            Destroy(_instancePhantomObject);
            _instancePhantomObject = null;
        }
    }
}
