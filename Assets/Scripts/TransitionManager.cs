using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class TransitionManager : MonoBehaviour
{
    public Image fadeScreen;
    public Image lockedImage;
    public float fadeDuration = 5;

    private void Start()
    {
        fadeScreen.color = new Color(0, 0, 0, 1);
        lockedImage.gameObject.SetActive(false);
        StartCoroutine(FadeOut());
    }

    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(TransitionToScene(sceneIndex));
    }

    private IEnumerator TransitionToScene(int sceneIndex)
    {
        lockedImage.gameObject.SetActive(true);
        yield return FadeIn();
        SceneManager.LoadScene(sceneIndex);
        yield return FadeOut();
        lockedImage.gameObject.SetActive(false);
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float alpha = elapsedTime / fadeDuration;
            fadeScreen.color = new Color(0, 0, 0, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        fadeScreen.color = new Color(0, 0, 0, 1);
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float alpha = 1 - (elapsedTime / fadeDuration);
            fadeScreen.color = new Color(0, 0, 0, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        fadeScreen.color = new Color(0, 0, 0, 0);
    }
}