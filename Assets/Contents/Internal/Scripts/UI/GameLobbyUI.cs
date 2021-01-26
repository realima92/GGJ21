using UnityEngine;
using Network;

public class GameLobbyUI : MonoBehaviour
{
    public void PlayOffline()
    {
        GameManager.StartSingleplayer();
    }

    public void JoinRoom()
    {
        NetworkManager.JoinRoom();
    }

    public void BackButton()
    {
        GameManager.Instance.LoadController.BackScene();
    }
}
