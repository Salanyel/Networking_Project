using UnityEngine;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {

    /// <summary>
    /// Value for the max health
    /// </summary>
    public const int c_maxHealth = 100;

    public bool m_destroyOnDeath;
    [SyncVar(hook ="OnChangeHealth")]
    public int m_currentHealth = c_maxHealth;

    public RectTransform m_healthBar;

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

    void OnChangeHealth(int p_health)
    {
        m_healthBar.sizeDelta = new Vector2(p_health, m_healthBar.sizeDelta.y);
    }

    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            //Move back to zero Location
            transform.position = Vector3.zero;
        }
    }
}
