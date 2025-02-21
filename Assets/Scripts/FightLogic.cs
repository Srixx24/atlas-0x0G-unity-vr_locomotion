using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FightLogic : MonoBehaviour
{
    public int health = 100;
    private int enemyKillCount = 0;
    private const int killThreshold = 12; // Kills to spawn boss
    private SpawnManager spawnManager;
    private WeaponTrack weaponTrack;
    public GameObject swordShotEffectPrefab;
    public GameObject bowShotEffectPrefab;
    public GameObject deathEffectPrefab1;
    public GameObject deathEffectPrefab2;
    public InputActionReference shootAction;
    public GameObject lifeBarPrefab;
    private GameObject healthBar;

    void Start()
    {
        spawnManager = Object.FindFirstObjectByType<SpawnManager>();
        weaponTrack = Object.FindFirstObjectByType<WeaponTrack>();
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
        if (weaponTrack.currentWeapon.CompareTag("Bow"))
        {
            // Instantiate the shot effect at the bow's position and rotation
            GameObject shotEffect = Instantiate(bowShotEffectPrefab, weaponTrack.bowGrabPoint.position, weaponTrack.bowGrabPoint.rotation);

            // Raycast to check what the shot hits
            RaycastHit hit;
            if (Physics.Raycast(weaponTrack.bowGrabPoint.position, weaponTrack.bowGrabPoint.forward, out hit))
            {
                FightLogic target = hit.collider.GetComponent<FightLogic>();
                if (target != null)
                {
                    target.TakeDamage(20);
                    HandleWeaponKill("Bow");
                }
            }

            // Destroy shot effect after a short duration
            Destroy(shotEffect, 2f);
        }
        else if (weaponTrack.currentWeapon.CompareTag("Sword"))
        {
            // Instantiate the shot effect at the sword's position and rotation
            GameObject shotEffect = Instantiate(swordShotEffectPrefab, weaponTrack.swordGrabPoint.position, weaponTrack.swordGrabPoint.rotation);

            // Raycast to check what the shot hits
            RaycastHit hit;
            if (Physics.Raycast(weaponTrack.swordGrabPoint.position, weaponTrack.swordGrabPoint.forward, out hit))
            {
                FightLogic target = hit.collider.GetComponent<FightLogic>();
                if (target != null)
                {
                    target.TakeDamage(20);
                    HandleWeaponKill("Sword");
                }
            }

            // Destroy shot effect after a short duration
            Destroy(shotEffect, 2f);
        }
    }

    // Method to deal damage to this entity
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
            Die();
    }

    private void Die()
    {
        PlayDeathEffects(transform.position);
        Destroy(healthBar);
        Destroy(gameObject);
        HandleKill();
    }

    private void HandleKill()
    {
        enemyKillCount++;
        if (enemyKillCount >= killThreshold)
            spawnManager.SpawnBoss();
    }

    public void HandleWeaponKill(string weaponType)
    {
        switch (weaponType)
        {
            case "Sword":
                // Call gore effect
                Debug.Log("Sword kill");
                PlayGoreEffects();
                break;
            case "Bow":
                // Call magic effect
                Debug.Log("Bow kill");
                PlayMagicEffects();
                break;
        }
    }

    private void PlayGoreEffects()
    {
        Instantiate(swordShotEffectPrefab, transform.position, Quaternion.identity);
    }

    private void PlayMagicEffects()
    {
        Instantiate(bowShotEffectPrefab, transform.position, Quaternion.identity);
    }

    public void PlayDeathEffects(Vector3 position)
    {
        GameObject deathEffect = Random.Range(0, 2) == 0 ? deathEffectPrefab1 : deathEffectPrefab2;
        Instantiate(deathEffect, position, Quaternion.identity);
    }
}