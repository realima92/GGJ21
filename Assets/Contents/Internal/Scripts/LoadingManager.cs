using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    private GameObject loadingUI;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded();
    }

    public void EnterLobby()
    {
        loadingUI.SetActive(false);
        SceneManager.LoadScene("GameLobby");
    }

    private UnityAction<Scene, LoadSceneMode> OnSceneLoaded()
    {
        return new UnityAction<Scene, LoadSceneMode>((scene, mode) =>
        {
            loadingUI.SetActive(false);
        });
    }
}
