﻿using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour {

    private void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
}
