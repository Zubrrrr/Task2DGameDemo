using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _spawnInterval = 0.05f;

    private float _timer = 0f;

    public bool Fire()
    {
        if (_timer >= _spawnInterval)
        {
            SpawnBullet();
            _timer = 0f;
            return true;
        }

        return false;
    }

    private void SpawnBullet()
    {
        Instantiate(_bulletPrefab, _spawnPoint.position, _spawnPoint.rotation);
    }

    private void Update()
    {
        _timer += Time.deltaTime;
    }
}

