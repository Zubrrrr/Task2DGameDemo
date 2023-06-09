using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Shooting : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private WeaponRecoil _weaponRecoil;
    [SerializeField] private SpriteChanger _spriteChanger;
    [SerializeField] private float _nextFireTime = 0f;

    private bool _isShooting = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        _isShooting = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isShooting = false;
    }

    private void Update()
    {
        if (_isShooting && Time.time >= _nextFireTime)
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

        // Создаем пулю на позиции firePoint
        //Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Дополнительная логика для стрельбы
        // ...
    }
}
