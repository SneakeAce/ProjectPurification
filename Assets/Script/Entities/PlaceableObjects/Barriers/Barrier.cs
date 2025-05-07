using UnityEngine;
using Zenject;

public abstract class Barrier : MonoBehaviour, IBarrier
{
    protected BarrierConfig _config;
    protected BarrierHealth _barrierHealth;
    protected CoroutinePerformer _coroutinePerformer;

    public Transform Transform => transform;
    public Collider Collider => GetComponent<Collider>();
    public Animator Animator => throw new System.NotImplementedException();
    public Rigidbody Rigidbody => throw new System.NotImplementedException();

    public abstract void Initialization();

    public void SetComponents(BarrierConfig config)
    {
        _config = config;

        Initialization();
    }

}
