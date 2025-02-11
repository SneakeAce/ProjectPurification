using DG.Tweening;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public abstract class Weapon : MonoBehaviour
{
    private const float MinDelayBeforeFiring = 0.1f;

    [Header("Main parameters weapon")]
    [SerializeField] protected WeaponConfig _weaponConfig;
    [SerializeField] protected int _maxMagazineCapacity;

    [Header("Delay before firing")]
    [SerializeField] protected float _startDelayBeforeFiring;

    [Header("For Bullets Pool")]
    [SerializeField] protected Bullet _bulletPrefab;
    [SerializeField] protected int _maxPoolSize;

    protected int _currentMagazineCapacity;
    protected int _currentReleasedBulletAtTime = 1;

    protected float _delayBeforeFiring;

    protected bool _isReloading;
    private bool _isCanWork = false;

    protected GameObject _poolHolder;
    protected ObjectPool<Bullet> _bulletPool;

    protected Character _character;
    protected PlayerInput _playerInput;

    protected Coroutine _reloadingWeaponCoroutine;

    public WeaponConfig WeaponConfig => _weaponConfig;

    public int MaxMagazineCapacity { get => _maxMagazineCapacity; }
    public int CurrentMagazineCapacity { get => _currentMagazineCapacity;  }

    public event Action<int> MaxValueChanged;
    public event Action<int> CurrentValueChanged;

    protected abstract IEnumerator PrepareWeaponToShootingJob(); // Для анимации подготовки оружия к стрельбе
    protected abstract void Shooting(); // Для выстрела и анимации выстрела
    protected abstract IEnumerator ReloadingJob(float timeReload); // Для анимации перезарядки 

    public virtual void Initialize(Character character)
    {
        _character = character;
        _playerInput = character.PlayerInput;

        _currentMagazineCapacity = _maxMagazineCapacity;

        if (_startDelayBeforeFiring < MinDelayBeforeFiring)
            _startDelayBeforeFiring = MinDelayBeforeFiring;

        _delayBeforeFiring = _startDelayBeforeFiring;

        _isCanWork = true;
    }

    public void CurrentValueChange()
    {
        CurrentValueChanged?.Invoke(_currentMagazineCapacity);
    }

    protected void Update()
    {
        if (_isCanWork == false)
            return;

        if (_delayBeforeFiring > 0)
            _delayBeforeFiring -= Time.deltaTime;

        if ((_playerInput.PlayerShooting.Shoot.IsPressed() || _playerInput.PlayerShooting.Shoot.WasPressedThisFrame()) && _delayBeforeFiring <= 0 && _currentMagazineCapacity > 0)
        {
            _character.Animator.SetTrigger("Firing");

            Shooting();
        }
        else if (_currentMagazineCapacity == 0 && _isReloading == false)
        {
            _isReloading = true;

            if (_reloadingWeaponCoroutine != null)
            {
                StopCoroutine(_reloadingWeaponCoroutine);
                _reloadingWeaponCoroutine = null;
            }

            _reloadingWeaponCoroutine = StartCoroutine(ReloadingJob(WeaponConfig.WeaponStatsConfig.ReloadingTime));
        }

        // Написать метод для стрельбы с зажатой клавишей, одиночными встрелами и выстрелами очередью. Через Enum и нажатие клавиши
    }

}
