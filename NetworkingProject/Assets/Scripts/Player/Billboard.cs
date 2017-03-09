using UnityEngine;
using System.Collections;

/// <summary>
/// Class to rotate a UIElement in the world to face the main camera
/// </summary>
public class Billboard : MonoBehaviour {

    private void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
}
