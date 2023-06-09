using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    [SerializeField] private float _recoilDistance = 0.1f;
    [SerializeField] private float _recoilSpeed = 0.35f;           

    private Vector3 _initialPosition;         
    private bool _isRecoiling = false;

    public void ApplyRecoil()
    {
        if (!_isRecoiling)
        {
            _isRecoiling = true;
        }
    }

    private void Start()
    {
        _initialPosition = transform.localPosition;
    }

    private void Update()
    {
        if (_isRecoiling)
        {
            transform.Translate(Vector3.left * _recoilSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.localPosition, _initialPosition) >= _recoilDistance)
            {
                _isRecoiling = false;
                transform.localPosition = _initialPosition;
            }
        }
    }
}
