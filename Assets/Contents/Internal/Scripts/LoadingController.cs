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

    private static LoadingController _instance;
    public static LoadingController Instance => _instance;

    private AsyncOperation ops;

    // Start is called before the first frame update
    void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded();
        }
        else if(_instance != this)
        {
            Destroy(gameObject);
        }
    }

    internal static void LoadAddictive(int index, Action<AsyncOperation> onLoad)
    {
        if(index == SceneManager.GetActiveScene().buildIndex)
        {
            Debug.LogError("fix buildindex");
            return;
        }
        _instance.ops = SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);
        _instance.ops.allowSceneActivation = false;
        _instance.ops.completed += onLoad;
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
    }
}
