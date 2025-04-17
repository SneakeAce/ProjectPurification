using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public abstract class Weapon : MonoBehaviour, IWeapon
{
    protected const int ReleasedBulletsOfSingleShootingMode = 1;
    protected const int MinMagazineCapacity = 0;
    private const float MinDelayBeforeFiring = 0.1f;

    protected Character _character;
    protected PlayerInput _playerInput;

    protected WeaponConfig _weaponConfig;
    protected InputWeaponHandler _inputWeaponAttackHandler;

    protected IFactory<Bullet, BulletType> _bulletFactory;
    protected SpawnPointBullet _spawnPointBullet;

    protected Coroutine _reloadingWeaponCoroutine;
    protected Coroutine _firingWeaponCoroutine;

    protected int _maxMagazineCapacity;
    protected int _currentMagazineCapacity;

    protected float _startDelayBeforeFiring;
    protected float _delayBeforeFiring;
    protected float _baseReloadingTime;
    protected float _baseShootingRange;

    protected bool _isReloading;

    private int _currentFiringModeIndex;

    private Dictionary<FiringMode, IFiringModeStrategy> _firingModeStrategies = new Dictionary<FiringMode, IFiringModeStrategy>();

    private FiringMode _allowedFiringModesInWeapon;
    private FiringMode _currentFiringMode;
    private List<FiringMode> _listFiringModes;

    private BulletType _bulletTypeInCurrentWeapon;

    private bool _isCanFiring;
    private bool _isFiring;

    [Inject]
    private void Construct(PlayerInput playerInput, InputWeaponHandler inputWeaponAttackHandler, 
        IFactory<Bullet, BulletType> bulletFactory, List<IFiringModeStrategy> firingModeStrategies)
    {
        _playerInput = playerInput;
        
        _bulletFactory = bulletFactory;
        //_character = character;
        //_weaponConfig = weaponConfig;
        _inputWeaponAttackHandler = inputWeaponAttackHandler;

        foreach(IFiringModeStrategy firingMode in firingModeStrategies)
        {
            _firingModeStrategies.Add(firingMode.FiringMode, firingMode);
        }
    }

    public int MaxMagazineCapacity { get => _maxMagazineCapacity; }
    public int CurrentMagazineCapacity { get => _currentMagazineCapacity;  }

    public float DelayBeforeFiring => _delayBeforeFiring;
    public bool IsFiring => _isFiring;
    public bool IsCanFiring => _isCanFiring;

    public event Action<int> MaxValueChanged;
    public event Action<int> CurrentValueChanged;

    protected abstract IEnumerator PrepareWeaponToShootingJob(); // Для анимации подготовки оружия к стрельбе
    protected abstract void Shooting(); // Для выстрела и анимации выстрела
    protected abstract void SpawnBullet(Quaternion rotate);

    public void SetComponents(WeaponConfig weaponConfig, Character character)
    {
        _weaponConfig = weaponConfig;

        Debug.Log("WeaponConfig = " + _weaponConfig);

        _allowedFiringModesInWeapon = _weaponConfig.WeaponStatsConfig.FiringMode;

        _character = character;

        Initialize();
    }

    public void CurrentMagazineValueChange()
    {
        CurrentValueChanged?.Invoke(_currentMagazineCapacity);
    }

    public void StartShoot()
    {
        if (_currentMagazineCapacity <= MinMagazineCapacity)
            ReloadingWeapon();

        Shooting();
    }

    public void StopShoot()
    {
        StopFiring();
    }

    protected Bullet GetBullet(Quaternion rotation)
    {
        Bullet bullet = _bulletFactory.Create(_spawnPointBullet.transform.position, _bulletTypeInCurrentWeapon, rotation);

        if (bullet == null)
            return null;

        return bullet;
    }

    private void Initialize()
    {
        _spawnPointBullet = GetComponentInChildren<SpawnPointBullet>();

        _listFiringModes = GetAllowedFiringModes();

        _currentFiringMode = GetCurrentFiringMode();

        SubscribingEvents();

        InitializeWeaponCharacteristics();
    }

    private List<FiringMode> GetAllowedFiringModes()
    {
        List<FiringMode> firingModes = Enum.GetValues(typeof(FiringMode))
            .Cast<FiringMode>()
            .Where(mode => (mode & _allowedFiringModesInWeapon) != 0)
            .ToList();

        if (firingModes.Count <= 0)
            return new List<FiringMode>();

        return firingModes;
    }

    private FiringMode GetCurrentFiringMode()
    {
        _currentFiringModeIndex = 0;

        if (_listFiringModes.Count == 0)
            return FiringMode.AutomaticFireMode;

        FiringMode firingMode = _listFiringModes[_currentFiringModeIndex];

        return firingMode;
    }

    private void SubscribingEvents()
    {
        _inputWeaponAttackHandler.OnFireButtonDown += StartFiring;
        _inputWeaponAttackHandler.OnFireButtonUp += StopFiring;
        _inputWeaponAttackHandler.SwitchFireMode += SwitchFiringMode;
    }

    private void UnsubscribingEvents()
    {
        _inputWeaponAttackHandler.OnFireButtonDown-= StartFiring;
        _inputWeaponAttackHandler.OnFireButtonUp -= StopFiring;
        _inputWeaponAttackHandler.SwitchFireMode -= SwitchFiringMode;
    }

    private void InitializeWeaponCharacteristics()
    {
        _currentMagazineCapacity = _maxMagazineCapacity = _weaponConfig.WeaponStatsConfig.BaseMagazineCapacity;

        _startDelayBeforeFiring = _weaponConfig.WeaponStatsConfig.BaseDelayBeforeFiring;
        _delayBeforeFiring = Mathf.Clamp(_delayBeforeFiring, MinDelayBeforeFiring, _startDelayBeforeFiring);

        _baseReloadingTime = _weaponConfig.WeaponStatsConfig.BaseReloadingTime;
        _baseShootingRange = _weaponConfig.WeaponStatsConfig.BaseShootingRange;

        _bulletTypeInCurrentWeapon = _weaponConfig.WeaponStatsConfig.BulletTypeInWeapon;
    }

    private void StartFiring()
    {
        if (_isReloading || _isCanFiring == false)
            return;

        if (_firingWeaponCoroutine != null)
        {
            StopCoroutine(_firingWeaponCoroutine);
            _firingWeaponCoroutine = null;
        }

        _isFiring = true;

        _firingWeaponCoroutine = StartCoroutine(_firingModeStrategies[_currentFiringMode].FiringWeaponJob(this));

        _isCanFiring = false;
    }

    private void StopFiring()
    {
        _isFiring = false;

        if (_firingWeaponCoroutine != null)
        {
            StopCoroutine(_firingWeaponCoroutine);
            _firingWeaponCoroutine = null;
        }

        _isCanFiring = true;
    }

    private void ReloadingWeapon()
    {
        _isFiring = false;
        _isReloading = true;

        if (_reloadingWeaponCoroutine != null)
        {
            StopCoroutine(_reloadingWeaponCoroutine);
            _reloadingWeaponCoroutine = null;
        }

        _reloadingWeaponCoroutine = StartCoroutine(ReloadingJob(_baseReloadingTime));

        _isCanFiring = true;
    }

    private IEnumerator ReloadingJob(float reloadTime)
    {
        yield return new WaitForSeconds(reloadTime);

        _currentMagazineCapacity = _maxMagazineCapacity;
        CurrentMagazineValueChange();

        _isReloading = false;
    }

    private void SwitchFiringMode()
    {
        if (_listFiringModes.Count <= 1)
            return; 

        _currentFiringModeIndex = (_currentFiringModeIndex + 1) % _listFiringModes.Count;
        _currentFiringMode = _listFiringModes[_currentFiringModeIndex];

        // Корутина с анимацией переключения режима стрельбы.
    }

    private void OnDestroy()
    {
        UnsubscribingEvents();
    }
}
