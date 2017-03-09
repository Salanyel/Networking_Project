using UnityEngine;

/// <summary>
/// Bullet behavior
/// </summary>
public class Bullet : MonoBehaviour {

    /// <summary>
    /// Damage done by each bullet
    /// </summary>
    public int m_bulletDamage = 10;

    /// <summary>
    /// Destory the current bullet when colliding with an other object
    /// </summary>
    /// <param name="collision">Collision: other objects collided</param>
    private void OnCollisionEnter(Collision collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();

        if (health != null)
        {
            health.takeDamage(m_bulletDamage);
        }

        Destroy(gameObject);
    }

}
