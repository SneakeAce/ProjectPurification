using UnityEngine;

public class CharacterRotate : MonoBehaviour
{
    public void RotateCharacter(Character character)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            Vector3 targetPoint = hitInfo.point;
            targetPoint.y = 0;

            character.transform.LookAt(targetPoint);
        }

    }

}
