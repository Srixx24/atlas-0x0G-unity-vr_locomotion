using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public int health = 100;
    private RezManager rezManager;
    private Animator animator;

    void Start()
    {
        rezManager = Object.FindFirstObjectByType<RezManager>();
        animator = GetComponent<Animator>();
        animator.SetBool("isDead", false);
    }

    public void TakeDamage(GameObject enemy)
    {
        int damage = 0;

        // Damage check by enemy type
        if (enemy.CompareTag("Boss"))
            damage = 10;
        else if (enemy.CompareTag("Enemy"))
            damage = 5;

        // Apply damage
        health -= damage;
        Debug.Log("Player took damage: " + damage + ". Current health: " + health);

        // Health check
        if (health <= 0)
            PlayerDeath();
    }

    public void PlayerDeath()
    {
        animator.SetBool("isDead", true);
        if (rezManager != null)
            rezManager.RespawnPlayer();
    }
}
