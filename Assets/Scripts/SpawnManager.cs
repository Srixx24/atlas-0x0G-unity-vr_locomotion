using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemiesParent;
    public GameObject bossParent;


    void Start()
    {
        enemiesParent.SetActive(true);
        bossParent.SetActive(false);
    }

    public void ResetEnemies()
    {
        enemiesParent.SetActive(true);
        Debug.Log("Enemies activated!");
    }

    public void SpawnBoss()
    {
        bossParent.SetActive(true);
        Debug.Log("Boss spawned!");

        FightLogic bossLogic = bossParent.GetComponentInChildren<FightLogic>();
        if (bossLogic != null)
        {
            bossLogic.SetAsBoss();
        }
    }
}