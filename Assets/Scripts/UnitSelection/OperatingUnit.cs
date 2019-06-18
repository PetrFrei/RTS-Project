using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperatingUnit : MonoBehaviour
{
    [HideInInspector]
    public bool isOnPlace = true;
    [HideInInspector]
    public Vector3 lastPos;

    private void Start()
    {
        lastPos = transform.position;
    }
}
