using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurretPlacementSystem : ObjectPlacementSystem
{
    public CreatedPoolBarriersSystem PoolBarrierSystem => _createdPools.PoolBarrierSystem;

    public override void OnTogglePlacementMode(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _placingJob = !_placingJob;

            if (_placingJob)
            {
                _playerInput.UI.Disable();
                _playerInput.PlayerShooting.Disable();

                // вызов UI в игре
                _playerInput.PlacementObjectMode.ChooseTypeOfBarrirer.performed -= OnChooseTypePlacingObject;

                _playerInput.PlacementObjectMode.ChooseTypeOfBarrirer.performed += OnChooseTypePlacingObject;
            }
            else
            {
                ResetVariables();
            }
        }
    }

    public override void OnChooseTypePlacingObject(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (int.TryParse(context.control.name, out int keyNumber))
            {
                if (keyNumber >= 1)
                {
                    int stepBackInList = 1;
                    int selectedBarrierIndex = keyNumber - stepBackInList;

                    Debug.Log("OnChooseTypeBarrier / selectedBarrierIndex = " + selectedBarrierIndex);

                    if (Enum.IsDefined(typeof(BarriersType), selectedBarrierIndex))
                    {

                        BarriersType selectedType = (BarriersType)selectedBarrierIndex;
                        Debug.Log("OnChooseTypeBarrier / Enum IsDefined!!! / selectedType = " + selectedType);

                        if (PoolBarrierSystem.PoolDictionary.TryGetValue(selectedType, out ObjectPool<PlaceableObject> poolSelected))
                        {
                            Debug.Log("OnChooseTypeBarrier / Getting Value!!!");

                            _poolObject = poolSelected;

                            _currentPhantomObject = SelectedPhantomBarrier(selectedBarrierIndex);

                            _poolObjectSelected = true;
                        }

                    }
                }

            }
        }
    }

    public override void OnDeactivatePlacementMode(InputAction.CallbackContext context)
    {
        if (context.performed && _placingJob)
        {
            _placingJob = false;
            ResetVariables();
        }
    }

    public override void Initialization(Character character)
    {
        throw new NotImplementedException();
    }
}
