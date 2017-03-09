using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Class used to manage the health of a character
/// </summary>
public class Health : NetworkBehaviour {

    /// <summary>
    /// Value for the max health
    /// </summary>
    public const int c_maxHealth = 100;

    /// <summary>
    /// Set if the GameObject shall be destroyed on death
    /// </summary>
    public bool m_destroyOnDeath;

    /// <summary>
    /// Current health of the character
    /// </summary>
    [SyncVar(hook ="OnChangeHealth")]
    public int m_currentHealth = c_maxHealth;

    /// <summary>
    /// RectTransform to display the healthBar
    /// </summary>
    public RectTransform m_healthBar;

    /// <summary>
    /// Existing spawn points to respawn
    /// </summary>
    private NetworkStartPosition[] m_spawnPoints;

    /// <summary>
    /// Unity Start methods
    /// </summary>
    private void Start()
    {
        if (isLocalPlayer)
        {
            m_spawnPoints = FindObjectsOfType<NetworkStartPosition>();
        }
    }

    /// <summary>
    /// Function to take damage and triggered outside this class
    /// </summary>
    /// <param name="p_amount">int: amount to suppress</param>
    public void takeDamage(int p_amount)
    {

        //Only the server apply the damages
        if (!isServer)
        {
            return;
        }            

        m_currentHealth -= p_amount;

        if (m_currentHealth <= 0)
        {
            if (m_destroyOnDeath)
            {
                Destroy(gameObject);
            }
            else
            {
                m_currentHealth = c_maxHealth;

                //Called on the server, but invoked on the clients
                RpcRespawn();
            }            
        }
    }

    /// <summary>
    /// Change the health of the character
    /// </summary>
    /// <param name="p_health">int: value to soustracte</param>
    void OnChangeHealth(int p_health)
    {
        m_healthBar.sizeDelta = new Vector2(p_health, m_healthBar.sizeDelta.y);
    }

    /// <summary>
    /// Client function to repercute to the server to respawn the character
    /// </summary>
    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            //Set the spawnPoint to origin as a default value
            Vector3 spawnPoint = Vector3.zero;

            //If there is a spawn point array and the array is not empty, pick one at random
            if (m_spawnPoints != null && m_spawnPoints.Length > 0)
            {
                spawnPoint = m_spawnPoints[Random.Range(0, m_spawnPoints.Length)].transform.position;
            }

            //Set the player's position to the chosen spawn point
            transform.position = spawnPoint;
        }
    }
}
