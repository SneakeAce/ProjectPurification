using System.Collections.Generic;
using UnityEngine;

public interface ISpawnPointFactory
{
    List<SpawnPointForSpawner> Create(Transform holder);
}
