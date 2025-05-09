using System;

public interface IEntityHealth : IDamageable
{
    public abstract void Initialization(IEntity entity, IEntityConfig config);

    public abstract event Action<IEntity> EntityDied;
    public abstract event Action<float> CurrentValueChanged;
    public abstract event Action<float> MaxValueChanged;
}
