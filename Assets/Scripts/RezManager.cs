using UnityEngine;

public class RezManager : MonoBehaviour
{
    public Transform respawnPoint;
    public GameObject player;

    public void RespawnPlayer()
    {
        if (player != null && respawnPoint != null)
        {
            // Move player to respawn point
            player.transform.position = respawnPoint.position;
            player.transform.rotation = respawnPoint.rotation;
        }
    }
    public void SetRespawnPoint(Transform newRespawnPoint)
    {
        respawnPoint = newRespawnPoint;
    }
}