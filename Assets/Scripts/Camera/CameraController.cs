using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _dumping = 1.5f;
    [SerializeField] private Vector2 _offset = new Vector2(2f, 1f);
    [SerializeField] private string _tag;

    private bool _isLeft;
    private int _lastX;
    private Transform _player;
    private Vector3 _target;

    public void FindPlayer(bool playerIsLeft)
    {
        _player = GameObject.FindGameObjectWithTag(_tag).transform;
        _lastX = Mathf.RoundToInt(_player.position.x);

        if (playerIsLeft)
        {
            transform.position = new Vector3(_player.position.x - _offset.x, _player.position.y - _offset.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(_player.position.x + _offset.x, _player.position.y + _offset.y, transform.position.z);
        }
    }

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _offset = new Vector2(Mathf.Abs(_offset.x), _offset.y);

        FindPlayer(_isLeft);
    }

    private void Update()
    {
        if (_player)
        {
            int currentX = Mathf.RoundToInt(_player.position.x);

            if (currentX > _lastX)
            {
                _isLeft = false;
            }
            else if (currentX < _lastX)
            {
                _isLeft = true;
            }

            _lastX = Mathf.RoundToInt(_player.position.x);

            if (_isLeft)
            {
                _target = new Vector3(_player.position.x - _offset.x, _player.position.y + _offset.y, transform.position.z);
            }
            else
            {
                _target = new Vector3(_player.position.x + _offset.x, _player.position.y + _offset.y, transform.position.z);
            }

            Vector3 currentPosition = Vector3.Lerp(transform.position, _target, _dumping * Time.deltaTime);

            transform.position = currentPosition;
        }
    }
}
