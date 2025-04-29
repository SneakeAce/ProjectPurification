using System.Collections.Generic;
using UnityEngine;

public interface ISpawnPointFactory
{
    List<SpawnPoint> Create(Transform holder);
}
