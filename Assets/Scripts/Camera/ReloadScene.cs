using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private string _sceneName;

    public void OnPointerDown(PointerEventData eventData)
    {
        StartCoroutine(Restart(_sceneName));
    }

    private IEnumerator Restart(string name)
    {
        float fadeTime = Camera.main.GetComponent<Fading>().Fade(1f);

        yield return new WaitForSeconds(fadeTime);

        SceneManager.LoadScene(name);
    }
}
