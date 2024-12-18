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
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveX, 0.0f, moveZ);

        moveDirection.y = 0;

        Vector3 moveVelocity = moveDirection.normalized * _speed;

        _character.Rigidbody.velocity = new Vector3(moveVelocity.x, moveVelocity.y, moveVelocity.z);
        _character.Animator.SetFloat("MoveSpeed", moveDirection.magnitude);
        _character.Animator.SetFloat("MoveX", moveX);
        _character.Animator.SetFloat("MoveZ", moveZ);
    }
}
