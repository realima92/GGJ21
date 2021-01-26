using UnityEngine;
using Network;
using System;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance => _instance;

    public LoadingController LoadController;

    // Start is called before the first frame update
    void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }else if(_instance != this)
        {
            Destroy(gameObject);
        }
    }

    public static void StartSingleplayer()
    {
        Instance.LoadController.StartGame();
    }

    public static void ExitGame()
    {
        NetworkManager.DisconnectService();
        Debug.Log($"[GameManager] Bye Bye!");
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#endif
    }
}
