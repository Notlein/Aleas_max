using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshColliderTest : MonoBehaviour
{
    [SerializeField] private GameObject mappedDeco;

    void Start() 
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            // Create a ray from the camera through the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            RaycastHit hit;

            
            // Perform the raycast
            if (Physics.Raycast(ray, out hit))
            {
                // The 'hit' variable contains information about the GameObject hit by the ray
                GameObject clickedObject = hit.collider.gameObject;

                // Get and log the name of the clicked object
                string objectName = clickedObject.name;
                Debug.Log("Clicked on: " + mappedDeco);
            }
        }
    }
}
