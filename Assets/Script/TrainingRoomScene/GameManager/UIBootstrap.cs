using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class UIBootstrap : MonoBehaviour
{
    [Header("Interface and UI")]
    [SerializeField] private BulletBar _bulletBar;
    [SerializeField] private HealthBar _healthBar;


    public void Initialization()
    {
        _healthBar.Initialize();
        _bulletBar.Initialize();
    }
}
