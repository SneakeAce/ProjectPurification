using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretHealth : IDamageable
{
    public TurretHealth()
    {
    }

    public bool TryTakeDamage(float damage)
    {
        return true;
    }

    public void TakeDamage(float damage)
    {
        if (TryTakeDamage(damage) == false)
            return;


    }

}
