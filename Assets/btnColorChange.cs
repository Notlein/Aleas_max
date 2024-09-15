using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btnColorChange : SingletonMonoBehaviour<btnColorChange>
{

    public List<GameObject> btns = new List<GameObject>();
    public SceneVars scnvrs;
    private GameObject[] temp;
    // Start is called before the first frame update
    void Start()
    {

        scnvrs = GameObject.FindGameObjectsWithTag("SceneVars")[0].GetComponent<SceneVars>();


        temp = GameObject.FindGameObjectsWithTag("UI-hide");
        foreach (var btn in temp)
        {
            if (btn.name.StartsWith("btn"))
            {
                btn.GetComponent<Image>().color = scnvrs.scene_color;
            }

        }
        foreach (var b in btns)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
