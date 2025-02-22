using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject wereWolfPrefab;
    public GameObject harpyPrefab;
    public GameObject spiderPrefab;
    public GameObject treeIntPrefab;
    public GameObject bossPrefab;
    public int wereWolfCount = 3;
    public int harpyCount = 3;
    public int spiderCount = 4;
    public int treeIntCount = 2;
    private GameObject[] spawnArea;
    private GameObject bossSpawn;
    private List<GameObject> currentEnemies = new List<GameObject>();


    void Start()
    {
        spawnArea = GameObject.FindGameObjectsWithTag("Spawn Area");
        bossSpawn = GameObject.FindGameObjectWithTag("Boss Spawn Area");
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        SpawnEnemy(wereWolfPrefab, wereWolfCount);
        SpawnEnemy(harpyPrefab, harpyCount);
        SpawnEnemy(spiderPrefab, spiderCount);
        SpawnEnemy(treeIntPrefab, treeIntCount);
    }

    void SpawnEnemy(GameObject enemyPrefab, int count)
    {
        List<GameObject> availableSpawnAreas = new List<GameObject>(spawnArea);
        count = Mathf.Min(count, availableSpawnAreas.Count);

        for (int i = 0; i < count; i++)
        {
            // Pick a random spawn area
            int randomIndex = Random.Range(0, availableSpawnAreas.Count);
            GameObject selectedSpawnArea = availableSpawnAreas[randomIndex];

            // Get the spawn position
            Vector3 spawnPosition = GetSpawnPosition(selectedSpawnArea);

            // Instantiate the enemy
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            currentEnemies.Add(enemy);

            availableSpawnAreas.RemoveAt(randomIndex);
        }
    }

    Vector3 GetSpawnPosition(GameObject spawnArea)
    {
        Collider spawnCollider = spawnArea.GetComponent<Collider>();
        Vector3 randomPosition = new Vector3(
            Random.Range(spawnCollider.bounds.min.x, spawnCollider.bounds.max.x),
            0,
            Random.Range(spawnCollider.bounds.min.z, spawnCollider.bounds.max.z)
        );
        return randomPosition;
    }

    public void ResetEnemies()
    {
        // Destroy all currently spawned enemies
        foreach (GameObject enemy in currentEnemies)
            Destroy(enemy);

        currentEnemies.Clear();
        SpawnEnemies();
    }

    public void SpawnBoss()
    {
        if (bossSpawn != null)
        {
            Vector3 spawnPosition = bossSpawn.transform.position;
            Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
            Debug.Log("Boss spawned!");
        }
    }
}