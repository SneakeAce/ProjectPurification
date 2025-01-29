using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    public override void AddHealth(float value)
    {
        throw new NotImplementedException();
    }

    public override void DamageTaken(float damage)
    {
        _currentValue -= damage;

        if (_currentValue <= 0)
            DestroyUnit();
    }

    public override void DestroyUnit()
    {
        Destroy(gameObject);
    }
}
