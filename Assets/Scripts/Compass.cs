using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{
    public Transform player;
    public RectTransform compassPointer;
    public RectTransform compassBar;

    void Update()
    {
        Transform closestEnemy = FindClosestEnemy();
        if (closestEnemy != null)
        {
            Vector3 direction = closestEnemy.position - player.position;
            float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;

            // Map angle to compass bar position
            float normalizedAngle = (angle + 180) / 360;
            float pointerPosition = normalizedAngle * compassBar.sizeDelta.x;
            
            // Update compass pointer position
            compassPointer.anchoredPosition = new Vector2(pointerPosition - (compassBar.sizeDelta.x / 2), 0);
        }
    }

    Transform FindClosestEnemy()
    {
        Transform closest = null;
        float closestDistance = float.MaxValue;

         // Find all enemies by tags
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("WereWolf");
        enemies = CombineArrays(enemies, GameObject.FindGameObjectsWithTag("Harpy"));
        enemies = CombineArrays(enemies, GameObject.FindGameObjectsWithTag("Spider"));
        enemies = CombineArrays(enemies, GameObject.FindGameObjectsWithTag("TreeInt"));

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(player.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = enemy.transform;
            }
        }
        return closest;
    }

    // Helper method to combine arrays
    private GameObject[] CombineArrays(GameObject[] array1, GameObject[] array2)
    {
        GameObject[] combined = new GameObject[array1.Length + array2.Length];
        array1.CopyTo(combined, 0);
        array2.CopyTo(combined, array1.Length);
        return combined;
    }
}