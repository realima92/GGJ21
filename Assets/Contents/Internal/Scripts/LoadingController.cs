using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LoadingController : MonoBehaviour
{
    [SerializeField] private GameObject loadingUI;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded();
    }

    public void EnterLobby()
    {
        Invoke("ShowLoadingUI", .3f);
        SceneManager.LoadScene("GameLobby");
    }

    public void BackScene()
    {
        //Temporary code
        Invoke("ShowLoadingUI", .3f);
        SceneManager.LoadScene("MainMenu");
    }

    internal void StartGame()
    {
        Invoke("ShowLoadingUI", .3f);
        SceneManager.LoadScene("GamePlay");
    }

    private void ShowLoadingUI()
    {
        loadingUI.SetActive(true);
    }

    private UnityAction<Scene, LoadSceneMode> OnSceneLoaded()
    {
        return new UnityAction<Scene, LoadSceneMode>((scene, mode) =>
        {
            CancelInvoke("ShowLoadingUI");
            if (loadingUI && loadingUI.activeSelf) loadingUI.SetActive(false);
        });
    }
}
