using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    private const float MinSpeed = 0.0f;
    private const float MaxSpeed = 1.0f;

    [SerializeField] private Character _character;
    [SerializeField] private CharacterRotate _charaterRotate;
    [SerializeField] private float _speed;

    private void Update()
    {
        _charaterRotate.RotateCharacter(_character);

        Move();
    }
    
    private void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVecrtical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveHorizontal, 0.0f, moveVecrtical);
        moveDirection.y = 0;

        float moveSpeed = Mathf.Clamp(moveDirection.magnitude, MinSpeed, MaxSpeed);
        
        _character.transform.Translate(moveDirection * _speed * Time.deltaTime);

        _character.Animator.SetFloat("MoveSpeed", moveSpeed);
        _character.Animator.SetFloat("MoveX", moveHorizontal);
        _character.Animator.SetFloat("MoveZ", moveVecrtical);
    }
}
