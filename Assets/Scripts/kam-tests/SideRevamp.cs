using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SideRevamp : MonoBehaviour
{
    [SerializeField] private bool isDefaultRoot;
    [SerializeField] private int targetScene;
    [SerializeField] private Material root;

    [SerializeField] private List<Material> sides;

    [SerializeField] private Sprite backgroundImage;

    public Material GetRoot()
    {
        return root;
    }

    public List<Material> GetSides()
    {
        return sides;
    }

    public int GetTargetScene()
    {
        return targetScene;
    }

    public Sprite GetBackgroundImage()
    {
        return backgroundImage;
    }

    public bool IsDefaultRoot()
    {
        return isDefaultRoot;
    }

}
