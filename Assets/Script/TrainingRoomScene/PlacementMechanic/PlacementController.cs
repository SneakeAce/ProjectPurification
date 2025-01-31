using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementController : MonoBehaviour
{
    [SerializeField] private GameObject _placedObjectPrefab;
    [SerializeField] private GameObject _phantomObjectPrefab;
    [SerializeField] private Color _colorBeingPlacedObject;
    [SerializeField] private Color _colorNotBeingPlacedObject;
    [SerializeField] private float _radiusPlacing;
    [SerializeField] private float _objectRotationSpeed;

    private GameObject _instancePhantomObject;

    private Material _phantomObjectMaterial;
    private Color _baseColorPhantomObject;

    private Character _character;

    private bool _objectCanBePlaced;
    private bool _placingJob;
    private bool _canShowPhantomObject;

    public void Initialization(Character character)
    {
        _character = character;

        _placingJob = false;
        _canShowPhantomObject = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            _placingJob = true;
            Debug.Log("Input V, placingJob = " + _placingJob);
        }
        else if (_placingJob && Input.GetKeyDown(KeyCode.Escape))
        {
            _placingJob = false;
            ResetVariables();
        }

        if (_placingJob && _placedObjectPrefab != null)
        {
            Debug.Log("placingJob == true && prefab != null");
            Debug.Log("placingJob == true && prefab != null / canShowPhantomObject = " + _canShowPhantomObject);

            if (_canShowPhantomObject)
            {
                Debug.Log("CanShowPhantomObject true");

                ShowObjectPosition();

                float scroll = Input.GetAxis("Mouse ScrollWheel");

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
        Debug.Log("ShowObjectPosition, instancePhantomObject = " + _instancePhantomObject);

        if (_instancePhantomObject == null)
            _instancePhantomObject = Instantiate(_phantomObjectPrefab, _character.transform.position, Quaternion.identity);

        if (_phantomObjectMaterial == null)
        {
            _phantomObjectMaterial = _instancePhantomObject.GetComponent<MeshRenderer>().material;
            Debug.Log("phantomMaterial = " + _phantomObjectMaterial);

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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

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
        Debug.Log("ResetVariables");

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
