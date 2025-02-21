using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class EntryPoint : MonoBehaviour
{
    public Button startButton;
    public Button optionsButton;
    public Button exitButton;
    private AudioSource audioSource;
    public AudioClip buttonClickSound;


    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = buttonClickSound;
        startButton.onClick.AddListener(OnStartButtonClick);
        optionsButton.onClick.AddListener(OnOptionsButtonClick);
        exitButton.onClick.AddListener(OnExitButtonClicked);
    }

    private void PlayButtonClickSound()
    {
        if (audioSource.clip != null)
            audioSource.PlayOneShot(audioSource.clip);
    }

    private void OnStartButtonClick()
    {
        PlayButtonClickSound();
        SceneManager.LoadScene(1);
    }

    private void OnOptionsButtonClick()
    {
        //PlayButtonClickSound();
        //SceneManager.LoadScene();
    }

    private void OnExitButtonClicked()
    {
        PlayButtonClickSound();
        // Quit the application
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Debug.Log("Exiting application.");
    }
}
