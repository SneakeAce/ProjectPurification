using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveComponent : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private float _speed;

    private void FixedUpdate()
    {
        Move();
    }
    
    private void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(moveX, 0.0f, moveZ);

        _character.Rigidbody.AddForce(movement * _speed);
    }
}
