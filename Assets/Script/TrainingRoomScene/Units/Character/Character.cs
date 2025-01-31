using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : Unit
{
    private PlayerInput _playerInput;

    public PlayerInput PlayerInput => _playerInput;

    public void Initialization()
    {
        _playerInput = new PlayerInput();

        _playerInput.Enable();
    }
}
