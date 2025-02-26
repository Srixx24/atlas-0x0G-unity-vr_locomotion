using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class EntryPoint : MonoBehaviour
{
    public Button startButton;
    public Button exitButton;
    public Button mainMenuButton;
    private AudioSource audioSource;
    public AudioClip buttonClickSound;
    public TransitionManager transitionManager;
    public GameObject parentMainMenu;
    public GameObject parentEndGame;


    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = buttonClickSound;
        startButton.onClick.AddListener(OnStartButtonClick);
        exitButton.onClick.AddListener(OnExitButtonClicked);
        mainMenuButton.onClick.AddListener(OnReturnToMainMenuButtonClick);
        parentMainMenu.SetActive(true);
        parentEndGame.SetActive(false);
    }

    private void PlayButtonClickSound()
    {
        if (audioSource.clip != null)
            audioSource.PlayOneShot(audioSource.clip);
    }

    private void OnStartButtonClick()
    {
        PlayButtonClickSound();
        transitionManager.LoadScene(1);
    }

    private void OnReturnToMainMenuButtonClick()
    {
        PlayButtonClickSound();
        parentMainMenu.SetActive(true);
        parentEndGame.SetActive(false);
        Object.FindFirstObjectByType<EndGame>().SetPlayerPosition(gameObject);
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
