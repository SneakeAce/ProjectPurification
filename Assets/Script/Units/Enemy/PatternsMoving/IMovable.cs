using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovable
{
    Transform Transform { get; }

    float MoveSpeed { get; }

    Animator Animator { get; }
}
