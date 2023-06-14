using CodeMonkey.HealthSystemCM;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _lifetime = 3f;
    [SerializeField] private int _minDamage = 3;
    [SerializeField] private int _maxDamage = 6;
    [SerializeField] private float _maxDistance = 10f; 

    private int _damage;
    private Transform _playerTransform;
    private float _sqrMaxDistance; 
    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.right * _bulletSpeed;
        _damage = Random.Range(_minDamage, _maxDamage + 1);
        Destroy(gameObject, _lifetime);

        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _sqrMaxDistance = _maxDistance * _maxDistance;
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.TryGetComponent(out HealthEnemy enemy))
        {
            if (IsWithinFieldOfView(enemy.transform.position))
            {
                enemy.Damage(_damage);
            }

            Destroy(gameObject);
        }
    }

    private bool IsWithinFieldOfView(Vector3 position)
    {
        float sqrDistance = (_playerTransform.position - position).sqrMagnitude;
        return sqrDistance <= _sqrMaxDistance;
    }
}
