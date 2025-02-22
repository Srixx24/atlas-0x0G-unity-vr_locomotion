using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerLife : MonoBehaviour
{
    public int health = 100;
    private const int maxHealth = 100;
    private RezManager rezManager;
    private SpawnManager spawnManager;
    public Image lifeBarImage;
    public GameObject deathCanvas;



    void Start()
    {
        rezManager = Object.FindFirstObjectByType<RezManager>();
        spawnManager = Object.FindFirstObjectByType<SpawnManager>();
        UpdateLifeBar();
        if (deathCanvas != null)
            deathCanvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Collider check
        if (other.CompareTag("HitBox"))
        {
            GameObject enemy = other.transform.parent.gameObject; 
            TakeDamage(enemy);
        }
    }

    public void TakeDamage(GameObject enemy)
    {
        int damage = 0;

        // Enemy check
        if (enemy.CompareTag("Boss"))
            damage = 10;
        else if (enemy.CompareTag("Enemy"))
            damage = 5;

        // Apply damage
        health -= damage;
        health = Mathf.Max(health, 0);
        Debug.Log("Player took damage: " + damage + ". Current health: " + health);

        UpdateLifeBar();

        // Health check
        if (health <= 0)
            PlayerDeath();
    }

    private void UpdateLifeBar()
    {
        if (lifeBarImage != null)
            lifeBarImage.fillAmount = (float)health / 100;
    }

    private void ResetLifeBar()
    {
        health = maxHealth;
        UpdateLifeBar();;
    }

    public void PlayerDeath()
    {
        if (deathCanvas != null)
            deathCanvas.SetActive(true);

        StartCoroutine(HandleRespawn());
    }

    private IEnumerator HandleRespawn()
    {
        yield return new WaitForSeconds(3f);

        if (spawnManager != null)
            spawnManager.ResetEnemies();

        if (rezManager != null)
            rezManager.RespawnPlayer();

        if (deathCanvas != null)
            deathCanvas.SetActive(false);
        
        ResetLifeBar();
    }
}
