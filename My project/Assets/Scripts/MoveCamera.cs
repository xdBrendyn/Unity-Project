using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform CameraPos;

    void Update() {
        transform.position = CameraPos.position;    
    }
}
