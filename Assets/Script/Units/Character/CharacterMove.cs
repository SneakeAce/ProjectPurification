using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MoveComponent
{
    private const float MinSpeed = 0.0f;
    private const float MaxSpeed = 1.0f;

    public override void RotateToTarget(Unit character)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            Vector3 targetPoint = hitInfo.point;
            targetPoint.y = 0;

            character.transform.LookAt(targetPoint);
        }
    }

    public override void Move(Unit character)
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVecrtical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveHorizontal, 0.0f, moveVecrtical);
        moveDirection.y = 0;

        float moveSpeed = Mathf.Clamp(moveDirection.magnitude, MinSpeed, MaxSpeed);

        character.transform.Translate(moveDirection * _speed * Time.deltaTime);

        character.Animator.SetFloat("MoveSpeed", moveSpeed);
        character.Animator.SetFloat("MoveX", moveHorizontal);
        character.Animator.SetFloat("MoveZ", moveVecrtical);
    }
}
