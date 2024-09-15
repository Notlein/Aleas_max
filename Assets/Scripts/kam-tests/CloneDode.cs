using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneDode : MonoBehaviour
{
    [SerializeField] private List<GameObject> sides;

    public List<GameObject> GetSides()
    {
        return sides;
    }
}
