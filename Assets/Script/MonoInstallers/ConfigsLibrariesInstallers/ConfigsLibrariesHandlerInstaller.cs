using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class ConfigsLibrariesHandlerInstaller : MonoInstaller
{
    [SerializeField] private List<ScriptableObject> _libraryConfigs;

    public override void InstallBindings()
    {
        BindAllLibrariesConfig();
    }

    private void BindAllLibrariesConfig()
    {
        foreach (var config in _libraryConfigs)
        {
            if (config is ILibraryConfig == false)
            {
                Debug.Log($"This {config.name} is not ILibraryConfig. Return;");
                return;
            }

            Type libraryType = config.GetType();
            Type baseGeneric = libraryType.BaseType;

            if (baseGeneric == null || baseGeneric.IsGenericType == false)
            {
                Debug.LogWarning($"[UniversalLibraryInstaller] {config.name} не наследуется от LibraryConfigs<T>. Пропускаем.");
                continue;
            }

            Type configType = baseGeneric.GetGenericArguments()[0];

            Type enumType = configType.GetInterfaces()
                .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IConfigWithType<>))
                ?.GetGenericArguments()[0];

            if (enumType == null)
            {
                Debug.LogWarning($"[UniversalLibraryInstaller] {configType.Name} не реализует IConfigWithType<TEnum>. Пропускаем.");
                continue;
            }

            Type libraryBindType = typeof(LibraryConfigs<>).MakeGenericType(configType);
            Type handlerType = typeof(ConfigsLibrariesHandler<,>).MakeGenericType(configType, enumType);

            Container.Bind(libraryBindType)
                .FromInstance(config)
                .AsSingle();

            Container.Bind(handlerType)
                .AsSingle();
        }
    }
}
