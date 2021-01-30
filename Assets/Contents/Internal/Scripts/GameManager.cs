using UnityEngine;
using Network;
using System;
using System.Collections;

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

    public static IEnumerator ExitGame(float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log($"[GameManager] Bye Bye!");
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#endif
    }
}
