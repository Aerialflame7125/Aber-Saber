using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnyThing : MonoBehaviour
{
    public Vector3 rotationVector; 

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationVector.x * Time.deltaTime, rotationVector.y * Time.deltaTime, rotationVector.z * Time.deltaTime);
    }
}