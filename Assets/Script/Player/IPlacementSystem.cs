using System;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IPlacementSystem
{
    public bool PlacingJob { get; }

    public event Action StopWork;
    public event Action<GameObject> CreatePhantomObject;
    public event Action<GameObject> DestroyPhantomObject;

    void EnterMode(InputAction.CallbackContext context);
    void ChooseTypeOfPlacingObject(InputAction.CallbackContext context);
    void ExitMode();
    void Work();

}
