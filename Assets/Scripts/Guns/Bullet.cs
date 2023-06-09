using CodeMonkey.HealthSystemCM;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private int _minDamage = 3;
    [SerializeField] private int _maxDamage = 6;

    private int _damage;

    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.right * _bulletSpeed;
        _damage = Random.Range(_minDamage, _maxDamage + 1);
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.TryGetComponent(out HealthEnemy enemy))
        {
            enemy.Damage(_damage);
            Destroy(gameObject);
        }
    }
}
