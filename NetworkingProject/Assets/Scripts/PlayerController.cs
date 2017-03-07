using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerController : NetworkBehaviour {

    public float m_scaleHorizontal = 150.0f;
    public float m_scaleVertical = 3.0f;

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
    }

    /// <summary>
    /// Override: Behavior only done by the local player at the start
    /// </summary>
    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;
    }
}
