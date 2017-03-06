using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float m_scaleHorizontal = 150.0f;
    public float m_scaleVertical = 3.0f;

    // Update is called once per frame
    void Update () {
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * m_scaleHorizontal;
        float z = Input.GetAxis("Vertical") * Time.deltaTime * m_scaleVertical;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);
    }
}
