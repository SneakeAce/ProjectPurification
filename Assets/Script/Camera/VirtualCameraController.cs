using Cinemachine;
using UnityEngine;
using Zenject;

public class VirtualCameraController : MonoBehaviour,IVirtualCamera
{
    private CinemachineVirtualCamera _virtualCamera;
    private Character _character;

    [Inject]
    public void Construct(CinemachineVirtualCamera virtualCamera, Character character)
    {
        _virtualCamera = virtualCamera;
        _character = character;

        SetTargetForCamera();
    }

    public void SetTargetForCamera()
    {
        _virtualCamera.Follow = _character.transform;
    }
}
