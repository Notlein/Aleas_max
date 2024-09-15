using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class NormalCorrector : MonoBehaviour
{
    void Start()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.RecalculateNormals();  // Force Unity to recalculate normals based on the mesh geometry
    }
}
