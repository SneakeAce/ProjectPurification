using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarLookAtCamera : MonoBehaviour
{
    [SerializeField] private Unit _character;
    [SerializeField] private GameObject _healthBar;
    [SerializeField] private Camera _camera;

    private Vector3 _offset;

    private void Start()
    {
        _offset = _healthBar.transform.position;    
    }

    private void Update()
    {
        if (_character != null)
        {
            transform.position = _healthBar.transform.position;
            transform.rotation = Quaternion.LookRotation(transform.position - _camera.transform.position, Vector3.up);
        }    
    }
}
