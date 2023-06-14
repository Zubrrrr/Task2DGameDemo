using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprites; 
    [SerializeField] private float _changeInterval = 1f;

    private SpriteRenderer _spriteRenderer;
    private int _currentSpriteIndex = 0;
    private float _timer = 0f;

    public void StartChange()
    {
        if (!gameObject.activeInHierarchy) return;

        _spriteRenderer.enabled = true;
        _timer += Time.deltaTime;

        if (_timer >= _changeInterval)
        {
            _timer = 0f;
            _currentSpriteIndex = (_currentSpriteIndex + 1) % _sprites.Length;
            _spriteRenderer.sprite = _sprites[_currentSpriteIndex];
        }
    }

    public void StopChange()
    {
        if (!gameObject.activeInHierarchy) return;

        _spriteRenderer.enabled = false;
        _currentSpriteIndex = 0;
        _spriteRenderer.sprite = _sprites[_currentSpriteIndex];
    }

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.enabled = false;

        if (_sprites.Length == 0)
        {
            Debug.LogWarning("Нет спрайтов, назначенных скрипту SpriteChanger на объекте:" + gameObject.name);
        }
        else
        {
            _spriteRenderer.sprite = _sprites[_currentSpriteIndex];
        }
    }
}

