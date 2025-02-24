using Zenject;

public class GlobalInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindPlayerInput();
    }

    private void BindPlayerInput()
    {
        Container.Bind<PlayerInput>().AsSingle();

    }
}
