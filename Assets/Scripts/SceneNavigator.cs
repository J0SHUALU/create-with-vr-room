using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigator : MonoBehaviour
{
    [SerializeField] CanvasGroup fadeGroup;
    [SerializeField] float fadeDuration = 0.6f;
    [SerializeField] AudioSource transitionSound;

    bool loading;

    void Start()
    {
        if (fadeGroup != null)
            StartCoroutine(Fade(1f, 0f));
    }

    public void LoadScene(string sceneName)
    {
        if (!loading)
            StartCoroutine(LoadRoutine(sceneName));
    }

    public void ReloadCurrentScene()
    {
        LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator LoadRoutine(string sceneName)
    {
        loading = true;
        if (transitionSound != null)
            transitionSound.Play();
        if (fadeGroup != null)
            yield return Fade(0f, 1f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone)
            yield return null;
    }

    IEnumerator Fade(float from, float to)
    {
        float elapsed = 0f;
        fadeGroup.alpha = from;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            fadeGroup.alpha = Mathf.Lerp(from, to, elapsed / fadeDuration);
            yield return null;
        }
        fadeGroup.alpha = to;
    }
}
