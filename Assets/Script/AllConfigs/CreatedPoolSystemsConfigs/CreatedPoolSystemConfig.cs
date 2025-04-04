using System.Collections.Generic;
using UnityEngine;

public class CreatedPoolSystemConfig<T> : ScriptableObject 
    where T : MonoBehaviour
{
    [field: SerializeField] public List<T> Objects { get; private set; }
}
