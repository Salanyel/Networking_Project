using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Class used to spawn enemies
/// </summary>
public class EnemySpawner : NetworkBehaviour
{

    /// <summary>
    /// Prefab to instantiate for the enemies
    /// </summary>
    public GameObject m_enemyPrefab;

    /// <summary>
    /// Enemies number to spawn
    /// </summary>
    public int m_numberOfEnemies;

    /// <summary>
    /// Override: function called on the server / Generate a random number of enemies
    /// </summary>
    public override void OnStartServer()
    {
        for (int i = 0; i < m_numberOfEnemies; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-8.0f, 8.0f), 0.0f, Random.Range(-8.0f, 8.0f));

            Quaternion spawnRotation = Quaternion.Euler(0.0f, Random.Range(0, 180), 0.0f);

            GameObject enemy = (GameObject)Instantiate(m_enemyPrefab, spawnPosition, spawnRotation);
            NetworkServer.Spawn(enemy);
        }
    }
}