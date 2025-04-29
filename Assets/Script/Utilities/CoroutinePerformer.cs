using UnityEngine;

public class CoroutinePerformer : MonoBehaviour
{
    private void Awake() => DontDestroyOnLoad(this);
}
