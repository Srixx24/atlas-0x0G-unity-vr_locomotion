using UnityEngine;

public class EnemyWander : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float wanderRadius = 5f;
    public float attackRange = 6f;
    public Transform playerTransform;
    private Vector3 targetPosition;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        float randomYRotation = Random.Range(0f, 360f);
        transform.rotation = Quaternion.Euler(0f, randomYRotation, 0f);

        animator.SetBool("isLooking", true);
        SetRandomTargetPosition();
    }

    private void Update()
    {
        // Check distance to player
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        bool playerInRange = distanceToPlayer < attackRange;

        animator.SetBool("playerInRange", playerInRange);

        // Handle state transitions
        if (animator.GetBool("isLooking"))
        {
            if (Time.time > 2f)
            {
                animator.SetBool("isLooking", false);
                animator.SetBool("isWalking", true);
            }
        }
        else if (animator.GetBool("isWalking"))
        {
            if (playerInRange)
            {
                // Move to player
                transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
                AttackPlayer();
            }
            else
            {
                // Wander if out of range
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
                    SetRandomTargetPosition();
            }
        }
    }

    private void AttackPlayer()
    {
        Vector3 turnToPlayer = playerTransform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(turnToPlayer);

        animator.SetBool("isWalking", true);
        animator.SetBool("isAttacking", true);
        animator.SetBool("playerInRange", true);

        // Apply damage
        PlayerLife playerLife = playerTransform.GetComponent<PlayerLife>();
        if (playerLife != null)
            playerLife.TakeDamage(gameObject);
    }

    private void SetRandomTargetPosition()
    {
        targetPosition = new Vector3(
            transform.position.x + Random.Range(-wanderRadius, wanderRadius),
            transform.position.y,
            transform.position.z + Random.Range(-wanderRadius, wanderRadius)
        );
    }

    public void Die()
    {
        animator.SetBool("isDead", true);
    }
}