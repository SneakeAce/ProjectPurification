using System;
using Unity.VisualScripting;
using UnityEngine;

public class Character : Unit
{
    private void Update()
    {
        MoveComponent.Move(this);

        MoveComponent.RotateToTarget(this);
    }

}
