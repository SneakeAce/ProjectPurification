using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleBullet : Bullet
{
    public override void DamageDeal(Unit unit)
    {
        EnemyHealth unitHealth = unit.GetComponent<EnemyHealth>();

        unitHealth.DamageTaken(_damage);
        // Сделать класс для ХП Врага отдельно.
    }
}
