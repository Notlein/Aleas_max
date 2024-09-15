using System;
using System.Collections.Generic;
using UnityEngine;

public class ClickDetector : MonoBehaviour
{
    // Mapping the fake ones and the deco object colliders
    [SerializeField] private List<GameObject> fakeColliders;
    [SerializeField] private List<GameObject> decoObjects;
    private Dictionary<string, GameObject> mappedPairs;

    [SerializeField] private DodeAnimation dodeAnimation;

    [SerializeField] private MeshRenderer pivotMeshRenderer;

    // Variables for detecting tiny drag
    private Vector3 mouseDownPosition;
    private float clickThreshold = 0.1f; // Threshold for drag detection (adjust as needed)

    void Start()
    {
        mappedPairs = new();

        for (int i = 0; i < fakeColliders.Count; i++)
        {
            mappedPairs.Add(fakeColliders[i].name, decoObjects[i]);
            dodeAnimation.AddToSidesList(decoObjects[i]);
        }
    }

    void Update()
    {
        // Detect when mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            mouseDownPosition = Input.mousePosition;
        }

        // Check for mouse release
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 mouseUpPosition = Input.mousePosition;
            float dragDistance = Vector3.Distance(mouseDownPosition, mouseUpPosition);

            // Only trigger click behavior if drag is very small
            if (dragDistance < clickThreshold)
            {
                // Create a ray from the camera through the mouse position
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                // Perform the raycast
                if (Physics.Raycast(ray, out hit))
                {
                    GameObject clickedObject = hit.collider.gameObject;
                    string objectName = clickedObject.name;

                    if (mappedPairs.ContainsKey(objectName) && mappedPairs[objectName] != null)
                    {
                        GameObject selectedSide = mappedPairs[objectName];
                        dodeAnimation.SetSide(selectedSide);
                    }
                }
            }
        }
    }
}
