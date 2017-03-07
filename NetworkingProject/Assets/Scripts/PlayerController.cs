using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerController : NetworkBehaviour {

    public float m_scaleHorizontal = 150.0f;
    public float m_scaleVertical = 3.0f;

    /// <summary>
    /// Fields for the bullet
    /// </summary>
    public GameObject m_bulletPrefab;
    public Transform m_bulletSpawn;
    public float m_bulletSpeed = 6.0f;
    public float m_bulletLifeTime = 2.0f;

    // Update is called once per frame
    void Update () {
        if (!isLocalPlayer)
        {
            return;

        }

        detectInput();

    }    

    /// <summary>
    /// Detects the input and displace the player
    /// </summary>
    void detectInput()
    {
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * m_scaleHorizontal;
        float z = Input.GetAxis("Vertical") * Time.deltaTime * m_scaleVertical;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            fire();
        }
    }

    void fire()
    {
        //Create the Bullet from the bullet prefab
        GameObject bullet = Instantiate(m_bulletPrefab, m_bulletSpawn.position, m_bulletSpawn.rotation) as GameObject;

        //Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * m_bulletSpeed;

        //Destory the bullet after a certain time
        Destroy(bullet, m_bulletLifeTime);
    }

    /// <summary>
    /// Override: Behavior only done by the local player at the start
    /// </summary>
    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;
    }
}
