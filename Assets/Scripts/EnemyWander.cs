using UnityEngine;

public class EnemyWander : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float wanderRadius = 5f;
    public float attackRange = 2f;
    public Transform playerTransform;
    private Vector3 targetPosition;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        float randomYRotation = Random.Range(0f, 360f);
        transform.rotation = Quaternion.Euler(0f, randomYRotation, 0f);

        // Start with Look Around
        animator.SetBool("isLooking", true);
        SetRandomTargetPosition();
    }

    private void Update()
    {
        // Check distance to player
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        // Handle state transitions
        if (animator.GetBool("isLooking"))
        {
            // If looking is done, switch to walking
            if (Time.time > 2f)
            {
                animator.SetBool("isLooking", false);
                animator.SetBool("isWalking", true);
            }
        }
        else if (animator.GetBool("isWalking"))
        {
            // Move towards the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Check if the player is within attack range
            if (distanceToPlayer < attackRange)
            {
                animator.SetBool("isWalking", false);
                animator.SetBool("isAttacking", true);
            }

            // Check if the enemy has reached the target position
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
                SetRandomTargetPosition();
        }
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