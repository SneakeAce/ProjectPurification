using Cinemachine;
using UnityEngine;
using Zenject;

public class CameraControllerInstaller : MonoInstaller
{
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    public override void InstallBindings()
    {
        BindCamera();
    }

    private void BindCamera()
    {
        Container.BindInstance(_virtualCamera).AsSingle();

        Container.BindInterfacesAndSelfTo<VirtualCameraController>().AsSingle();
    }
}
