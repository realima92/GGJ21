using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private List<PlayableAsset> Cuts;

    private InputAction input;

    private PlayableDirector _director;


    private void Awake()
    {
        _director = GetComponent<PlayableDirector>();
    }

    public void ExitGame()
    {
        StartCoroutine(GameManager.ExitGame(2.5f));
    }

    public void PlayCut(int cut_number)
    {
        _director.Stop();
        _director.Play(Cuts[cut_number]);
    }
}
