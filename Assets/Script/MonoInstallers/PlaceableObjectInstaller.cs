using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlaceableObjectInstaller : MonoInstaller
{
    [SerializeField] private PlaceableObjectConfig _config;

    public override void InstallBindings()
    {
        
    }

    private void BindConfig()
    {

    }
}
