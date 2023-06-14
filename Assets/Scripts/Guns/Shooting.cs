using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Shooting : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private WeaponRecoil _weaponRecoil;
    [SerializeField] private SpriteChanger _spriteChanger;
    [SerializeField] private BulletSpawner _bulletSpawner;
    [SerializeField] private TextMeshProUGUI _ammoText;
    [SerializeField] private int _maxAmmo;

    private int _currentAmmo;
    private bool _isShooting = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        _isShooting = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isShooting = false;
    }

    public void ReloadAmmo(int amount)
    {
        _currentAmmo += amount;
        UpdateAmmoText();
        SaveManager.SaveData(_currentAmmo);
    }

    private void Start()
    {
        _maxAmmo = SaveManager.LoadData<int>();
        _currentAmmo = _maxAmmo;
        UpdateAmmoText();
    }

    private void Update()
    {
        if (_isShooting && _currentAmmo > 0)
        {
            Shoot();
        }
        else
        {
            if (_spriteChanger != null)
                _spriteChanger.StopChange();
        }
    }

    private void Shoot()
    {
        if (_weaponRecoil != null)
            _weaponRecoil.ApplyRecoil();

        if (_spriteChanger != null)
            _spriteChanger.StartChange();

        if (_bulletSpawner != null)
        {
            if (_bulletSpawner.Fire())
            {
                DecreaseAmmo();
            }
        }
    }

    private void DecreaseAmmo()
    {
        _currentAmmo--;
        UpdateAmmoText();

        if (_currentAmmo == 0)
        {
            _isShooting = false;
        }

        SaveManager.SaveData(_currentAmmo);
    }

    private void UpdateAmmoText()
    {
        if (_ammoText != null)
        {
            _ammoText.text = "" + _currentAmmo.ToString();
        }
    }
}
