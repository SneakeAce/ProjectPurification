using UnityEngine;
using Zenject;

public class WoodBarrier : Barrier
{
    private BarrierAttack _barrierAttack;

    [Inject]
    private void Construct(BarrierHealth barrierHealth, BarrierAttack barrierAttack, 
        CoroutinePerformer coroutinePerformer)
    {
        _barrierHealth = barrierHealth;
        _barrierHealth.EntityDied += ReturnToPool;

        _barrierAttack = barrierAttack;

        _coroutinePerformer = coroutinePerformer;
    }

    public override void Initialization()
    {
        _barrierHealth.Initialization(this, _config);

        _barrierAttack.Initialization(_config, this);
    }
}
