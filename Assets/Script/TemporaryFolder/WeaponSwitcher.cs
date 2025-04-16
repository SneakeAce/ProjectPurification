using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSwitcher
{
    private List<Weapon> _weaponsList = new();

    private Weapon _currentWeapon;
    private PlayerInput _playerInput;

    public WeaponSwitcher(PlayerInput playerInput)
    {
        _playerInput = playerInput;
    }

    public void Initialize(List<Weapon> weaponsList, Weapon baseWeapon)
    {
        _currentWeapon = baseWeapon;
        _weaponsList = weaponsList;

        _playerInput.PlayerWeapon.SwitchWeapon.performed += OnWeaponSwitched;
    }

    private void OnWeaponSwitched(InputAction.CallbackContext context)
    {
        int index = Mathf.RoundToInt(context.ReadValue<float>()) - 1;

        if (index < 0 || index >= _weaponsList.Count)
            return;

        if (_currentWeapon != null)
            _currentWeapon.gameObject.SetActive(false);

        _currentWeapon = _weaponsList[index];
        _currentWeapon.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        _playerInput.PlayerWeapon.SwitchWeapon.performed -= OnWeaponSwitched;
    }
}
