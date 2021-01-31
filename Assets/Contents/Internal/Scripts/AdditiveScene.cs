using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdditiveScene : MonoBehaviour
{
    public int index = 1;
    public GameObject spawned;

    private void Awake()
    {
        LoadingController.LoadAddictive(index, OnLoad);
    }

    private void OnLoad(AsyncOperation ops)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(index));
        //Invoke("DelayedActivation", 1);
    }

    public void DelayedActivation()
    {
    }
}
