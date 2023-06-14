using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartSceneOnCollision : MonoBehaviour
{
    [SerializeField] private string _playerTag;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(_playerTag))
        {
            RestartScene();
        }
    }

    private void RestartScene()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneIndex);
    }
}
