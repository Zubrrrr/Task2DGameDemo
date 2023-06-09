using CodeMonkey.HealthSystemCM;
using UnityEngine;

public class HealthPlayer : MonoBehaviour, IGetHealthSystem
{
    [SerializeField] private float _healthlevel;

    private HealthSystem _healthSystem;

    public void Damage()
    {
        _healthSystem.Damage(5);
    }

    public HealthSystem GetHealthSystem()
    {
        return _healthSystem;
    }

    private void Awake()
    {
        _healthSystem = new HealthSystem(_healthlevel);
        _healthSystem.OnDead += HealthSystem_OnDead;
    }

    private void HealthSystem_OnDead(object sender, System.EventArgs e)
    {
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        _healthSystem.Damage(5);
        Debug.Log(_healthSystem);
    }
}
