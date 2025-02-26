using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public TransitionManager transitionManager;
    private AudioSource audioSource;
    public AudioClip completionSound;
    public GameObject parentMainMenu;
    public GameObject parentEndGame;
    public Transform menuSpawnPoint;
    public Transform spiritSpawnPoint;
    private bool hasReturnedFromGame = false;


    void Start()
    {
        parentMainMenu.SetActive(false);
        parentEndGame.SetActive(true);
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = completionSound;
    }

    public void ReturnOfTheSpirit()
    {
        audioSource.PlayOneShot(audioSource.clip);
        transitionManager.LoadScene(0);
        hasReturnedFromGame = true;
    }

    public void SetPlayerPosition(GameObject player)
    {
        if (hasReturnedFromGame)
            player.transform.position = spiritSpawnPoint.position;
        else
            player.transform.position = menuSpawnPoint.position;
    }
}
