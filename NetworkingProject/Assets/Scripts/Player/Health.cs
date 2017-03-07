using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    /// <summary>
    /// Value for the max health
    /// </summary>
    public const int c_maxHealth = 100;

    public int m_currentHealth = c_maxHealth;

    public RectTransform m_healthBar;

    /// <summary>
    /// Function to take damage and triggered outside this class
    /// </summary>
    /// <param name="p_amount">int: amount to suppress</param>
    public void takeDamage(int p_amount)
    {
        m_currentHealth -= p_amount;

        if (m_currentHealth <= 0)
        {
            Debug.Log("Dead!");
        }

        m_healthBar.sizeDelta = new Vector2(m_currentHealth, m_healthBar.sizeDelta.y);

    }
}
