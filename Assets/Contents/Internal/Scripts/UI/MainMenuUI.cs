using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public void StartPressed()
    {
        GameManager.Instance.LoadController.EnterLobby();
    }
    
    public void ExitGame()
    {
        GameManager.ExitGame();
    }
}
