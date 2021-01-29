using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingController : MonoBehaviour
{
    [SerializeField] private GameObject loadingUI;
    [SerializeField] private Image fadeUI;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded();
    }

    private UnityAction<Scene, LoadSceneMode> OnSceneLoaded()
    {
        return new UnityAction<Scene, LoadSceneMode>((scene, mode) =>
        {
            CancelInvoke("ShowLoadingUI");
            if (loadingUI && loadingUI.activeSelf) loadingUI.SetActive(false);
            if (fadeUI && fadeUI.IsActive()) FadeOut();
        });
    }

    private void ShowLoadingUI(bool withFadeScreen = false)
    {
        loadingUI.SetActive(true);
        if (withFadeScreen) FadeIn();
    }

    private void FadeIn()
    {
        fadeUI.gameObject.SetActive(true);
    }

    private void FadeOut()
    {
        fadeUI.gameObject.SetActive(true);
        Destroy(gameObject, 1);
    }
}
