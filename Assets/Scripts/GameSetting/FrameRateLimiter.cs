using UnityEngine;

public class FrameRateLimiter : MonoBehaviour
{
    [SerializeField] private int _targetFrameRate = 60;

    private void Awake()
    {
        Application.targetFrameRate = _targetFrameRate;
    }
}
