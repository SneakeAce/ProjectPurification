using System;
using UnityEngine;
using Zenject;

public abstract class ConfigsLibrariesHandlerInstaller<TConfig, TEnum> : MonoInstaller
    where TConfig : ScriptableObject, IConfigWithType<TEnum>
    where TEnum : Enum
{
    [SerializeField] private LibraryConfigs<TConfig> _libraryConfigs;

    public override void InstallBindings()
    {
        Container.Bind<LibraryConfigs<TConfig>>().FromInstance(_libraryConfigs).AsTransient();
        Container.Bind<ConfigsLibrariesHandler<TConfig, TEnum>>().AsSingle();
    }
}
