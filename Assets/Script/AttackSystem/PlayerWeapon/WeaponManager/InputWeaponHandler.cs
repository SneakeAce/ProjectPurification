using System;
using UnityEngine;
using Zenject;

public class InputWeaponHandler : MonoBehaviour
{
    private PlayerInput _playerInput;

    private bool _isCanWork = false;

    public event Action OnFireButtonDown;
    public event Action OnFireButtonUp;
    public event Action SwitchFireMode;

    [Inject]
    private void Construct(PlayerInput playerInput)
    {
        _playerInput = playerInput;
    }

    private void Start() => _isCanWork = true;

    private void Update()
    {
        if (_isCanWork == false)
            return;

        if (_playerInput.PlayerWeapon.Shoot.IsPressed())
            OnFireButtonDown?.Invoke();
        
        if (_playerInput.PlayerWeapon.Shoot.WasReleasedThisFrame())
            OnFireButtonUp?.Invoke();

        if (_playerInput.PlayerWeapon.SwitchFireMode.IsPressed())
            SwitchFireMode?.Invoke();
    }
}
