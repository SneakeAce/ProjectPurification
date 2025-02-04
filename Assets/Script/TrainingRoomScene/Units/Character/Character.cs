using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : Unit
{
    [SerializeField] private CharacterHealth _health;
    private PlayerInput _playerInput;

    public PlayerInput PlayerInput => _playerInput;

    public CharacterHealth Health => _health;

    public void Initialization()
    {
        _playerInput = new PlayerInput();

        _playerInput.Enable();
    }
}
