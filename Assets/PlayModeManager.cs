using UnityEngine;
using UnityEditor;
using System;

public class PlayModeManager : MonoBehaviour
{
    

    //[MenuItem("Custom/Quit Play Mode")]
    //public static void QuitPlayMode()
    //{
    //    if (EditorApplication.isPlaying)
    //    {
    //        EditorApplication.isPlaying = false;
    //    }
    //}

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            //QuitPlayMode();
            Application.Quit();
        }
    }


}
