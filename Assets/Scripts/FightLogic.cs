using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FightLogic : MonoBehaviour
{
    public int health = 100;
    private int enemyKillCount = 0;
    private const int killThreshold = 12; // Kills to spawn boss
    private SpawnManager spawnManager;
    public GameObject bowShotEffectPrefab;
    public GameObject deathEffectPrefab1;
    public GameObject deathEffectPrefab2;
    public InputActionReference shootAction;
    public GameObject lifeBarPrefab;
    private GameObject healthBar;
    private bool isBoss = false;
    private Coroutine healingCoroutine;

    void Start()
    {
        spawnManager = Object.FindFirstObjectByType<SpawnManager>();
        shootAction.action.Enable();

        if (lifeBarPrefab != null)
        {
            healthBar = Instantiate(lifeBarPrefab, transform.position + Vector3.up, Quaternion.identity);
            healthBar.transform.SetParent(transform);
            healthBar.transform.localPosition = new Vector3(0, 5, 0);
        }
    }

    void Update()
    {
        if (shootAction.action.triggered)
            Shoot();

        if (healthBar != null)
        {
            healthBar.transform.position = transform.position + Vector3.up * 3.5f; // Position above enemy
            Vector3 directionToCamera = Camera.main.transform.position - healthBar.transform.position;
            healthBar.transform.rotation = Quaternion.LookRotation(directionToCamera) * Quaternion.Euler(0, 180, 0); // Face camera

            // Update health bar UI
            Image healthBarImage = healthBar.GetComponentInChildren<Image>();
            if (healthBarImage != null)
                healthBarImage.fillAmount = (float)health / 100; // Update base on current health\
        }
    }

    void Shoot()
    {
        if (GameObject.FindGameObjectsWithTag("Bow").Length > 0)
        {
            // Instantiate the shot effect at the bow's position and rotation
            GameObject shotEffect = Instantiate(bowShotEffectPrefab, transform.position, transform.rotation);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                FightLogic target = hit.collider.GetComponent<FightLogic>();
                if (target != null)
                {
                    target.TakeDamage(20);
                    Instantiate(bowShotEffectPrefab, transform.position, Quaternion.identity);
                    Debug.Log("Target hit! - 20");
                }
            }
            // Destroy shot effect after a short duration
            Destroy(shotEffect, 2f);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        health = Mathf.Max(health, 0);
        if (health <= 0)
            Die();
    }

    private void Die()
    {
        PlayDeathEffects(transform.position);
        Destroy(healthBar);
        Destroy(gameObject);
        Debug.Log("Target died!");

        if (isBoss)
            Object.FindFirstObjectByType<EndGame>().ReturnOfTheSpirit();
        else
            HandleKill();
    }

    private void HandleKill()
    {
        enemyKillCount++;
        if (enemyKillCount >= killThreshold)
            spawnManager.SpawnBoss();
    }

    public void PlayDeathEffects(Vector3 position)
    {
        GameObject deathEffect = Random.Range(0, 2) == 0 ? deathEffectPrefab1 : deathEffectPrefab2;
        Instantiate(deathEffect, position, Quaternion.identity);
    }

    public void SetAsBoss()
    {
        isBoss = true;
        if (healingCoroutine == null)
            healingCoroutine = StartCoroutine(HealOverTime());
    }

    public IEnumerator HealOverTime()
    {
        while (health > 0)
        {
            health += 1;
            health = Mathf.Min(health, 100);
            yield return new WaitForSeconds(30f);
        }
    }
}