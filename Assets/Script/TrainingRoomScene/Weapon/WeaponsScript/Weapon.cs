using System;
using System.Collections;
using UnityEngine;
using Zenject;

public abstract class Weapon : MonoBehaviour
{
    // ������� ������ � ������ WeaponConfig
    // ������� ��� � ��������� ������
    //
    protected const int ReleasedBulletsOfSingleShootingMode = 1;
    private const float MinDelayBeforeFiring = 0.1f;

    protected WeaponConfig _weaponConfig;
    protected Bullet _bulletPrefab;
    protected BulletType _bulletTypeUsedInCurrentWeapon;

    protected GameObject _spawnPoint;

    protected Character _character;
    protected PlayerInput _playerInput;

    protected Coroutine _reloadingWeaponCoroutine;

    protected int _maxMagazineCapacity;
    protected int _currentMagazineCapacity;

    protected float _startDelayBeforeFiring;
    protected float _delayBeforeFiring;

    protected bool _isReloading;
    private bool _isCanWork = false;

    // ������� �������� ���� � ��������� �����!!!
    private CreatedPoolBulletsSystem _poolBullets;
    protected ObjectPool<Bullet> _bulletPool;

    public WeaponConfig WeaponConfig => _weaponConfig;

    public int MaxMagazineCapacity { get => _maxMagazineCapacity; }
    public int CurrentMagazineCapacity { get => _currentMagazineCapacity;  }

    public event Action<int> MaxValueChanged;
    public event Action<int> CurrentValueChanged;

    [Inject]
    private void Construct(Character character, WeaponConfig weaponConfig, PoolCreator poolCreator)
    {
        _character = character;
        _weaponConfig = weaponConfig;
        _poolBullets = poolCreator.PoolBulletsSystem;
    }

    protected abstract IEnumerator PrepareWeaponToShootingJob(); // ��� �������� ���������� ������ � ��������
    protected abstract void Shooting(); // ��� �������� � �������� ��������
    protected abstract IEnumerator ReloadingJob(float timeReload); // ��� �������� ����������� 

    public void Initialize()
    {
        _playerInput = character.PlayerInput;

        _bulletPool = GetPool();

        _spawnPoint = _weaponConfig.WeaponStatsConfig.SpawnPointBullets;
        _bulletTypeUsedInCurrentWeapon = _weaponConfig.WeaponStatsConfig.BulletTypeUsed;

        _currentMagazineCapacity = _maxMagazineCapacity = _weaponConfig.WeaponStatsConfig.MaxMagazineCapacity;

        _startDelayBeforeFiring = _weaponConfig.WeaponStatsConfig.DelayBeforeFiring;
        _delayBeforeFiring = Mathf.Clamp(_delayBeforeFiring, MinDelayBeforeFiring, _startDelayBeforeFiring);

        _isCanWork = true;
    }

    public void CurrentValueChange()
    {
        CurrentValueChanged?.Invoke(_currentMagazineCapacity);
    }

    private void Update()
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

        // �������� ����� ��� �������� � ������� ��������, ���������� ��������� � ���������� ��������. ����� Enum � ������� �������
    }

    private ObjectPool<Bullet> GetPool()
    {
        BulletType bulletType = (BulletType)_bulletTypeUsedInCurrentWeapon;

        if (_poolBullets.PoolDictionary.TryGetValue(bulletType, out ObjectPool<Bullet> poolSelected))
            return poolSelected;

        return null;
    }
}
