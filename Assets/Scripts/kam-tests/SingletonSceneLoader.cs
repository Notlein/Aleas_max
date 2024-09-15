using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SingletonSceneLoader : SingletonMonoBehaviour<SingletonSceneLoader>
{
    [SerializeField] private Scene targetScene;


    public void PrepareNextScene(Scene scene)
    {
        targetScene = scene;

        SceneManager.LoadScene(targetScene.name);
    }

    public void PrepareNextScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
