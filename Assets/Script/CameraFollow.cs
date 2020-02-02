using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;

    public Vector3 offset;

    void Start()
    {
        offset = transform.position - player.transform.position;
    }
    
    void Update()
    {
        var pos = player.transform.position;
        pos += offset;
        transform.position = pos;
    }
}
