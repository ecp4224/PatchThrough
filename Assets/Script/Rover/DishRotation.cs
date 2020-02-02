using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishRotation : MonoBehaviour
{
     public float RotationSpeed;


    // Update is called once per frame
    void Update()
    {
        //Always rotate based on whichever up the rover is in
        transform.Rotate(transform.parent.up * Time.deltaTime * RotationSpeed, Space.World);
    }
}
