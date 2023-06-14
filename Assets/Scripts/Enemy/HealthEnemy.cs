using CodeMonkey.HealthSystemCM;
using UnityEngine;

public class HealthEnemy : MonoBehaviour, IGetHealthSystem
{
    [SerializeField] private float _healthlevel;

    private HealthSystem _healthSystem;
    private EnemyDropper _dropper;

    public void Damage(int damage)
    {
        _healthSystem.Damage(damage);
    }

    public HealthSystem GetHealthSystem()
    {
        return _healthSystem;
    }

    private void Awake()
    {
        _healthSystem = new HealthSystem(_healthlevel);
        _healthSystem.OnDead += HealthSystem_OnDead;

        _dropper = GetComponent<EnemyDropper>();
    }

    private void HealthSystem_OnDead(object sender, System.EventArgs e)
    {
        if (_dropper != null)
        {
            _dropper.TryDropItems();
        }

        Destroy(gameObject);
    }
}
