using System;
using System.Collections.Generic;
using UnityEngine;

public class ConfigsLibrariesHandler<TConfig, TEnum>
    where TConfig : ScriptableObject, IConfigWithType<TEnum>
    where TEnum : Enum
{
    private List<TConfig> _objectConfigs;

    private Dictionary<TEnum, TConfig> _objectConfigsDictionary;

    public ConfigsLibrariesHandler(LibraryConfigs<TConfig> objectConfigs)
    {
        _objectConfigs = objectConfigs.Configs;

        Initialization();
    }

    public TConfig GetObjectConfig(TEnum objectType)
    {
        if (_objectConfigsDictionary.ContainsKey(objectType) == false)
            return null;

        TConfig config = _objectConfigsDictionary[objectType];

        return config;
    }

    private void Initialization()
    {
        _objectConfigsDictionary = new Dictionary<TEnum, TConfig>();

        foreach (TConfig config in _objectConfigs)
        {
            TEnum type = config.ConfigType;

            if (_objectConfigsDictionary.ContainsKey(type))
                continue;

            _objectConfigsDictionary.Add(type, config);
        }
    }
    

}
