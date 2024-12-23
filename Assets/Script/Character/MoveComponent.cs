using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveComponent : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private CharacterRotate _charaterRotate;
    [SerializeField] private float _speed;

    private void Update()
    {
        _charaterRotate.RotateCharacter(_character);
    }

    private void FixedUpdate()
    {
        Move();
    }
    
    private void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVecrtical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveHorizontal, 0.0f, moveVecrtical);
        moveDirection.y = 0;

        float moveSpeed = Mathf.Clamp(moveDirection.magnitude, 0.0f, 1.0f);
        
        _character.transform.Translate(moveDirection * _speed * Time.deltaTime);

        _character.Animator.SetFloat("MoveSpeed", moveSpeed);
        _character.Animator.SetFloat("MoveX", moveHorizontal);
        _character.Animator.SetFloat("MoveZ", moveVecrtical);
    }
}
