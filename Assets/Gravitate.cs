using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Gravitate : MonoBehaviour
{

    public Vector3 origin;
    public float rotationSpeed = 1.0f;
    public Vector3 rotationAxis = Vector3.up;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(origin, Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
