using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkUI : MonoBehaviour
{
    [SerializeField] private GameObject connectingUI;
    [SerializeField] private GameObject connectedUI;

    private void OnLobbyConnected()
    {
        connectingUI?.SetActive(false);
        connectedUI?.SetActive(true);
    }

    public void UICallConnection()
    {
        connectingUI?.SetActive(true);
        NetworkManager.ConnectService();
        NetworkManager.Service.lobbyConnectedEvent += OnLobbyConnected;
    }

    public void CloseTV()
    {
        connectingUI?.SetActive(false);
        connectedUI?.SetActive(false);
        NetworkManager.Service.lobbyConnectedEvent -= OnLobbyConnected;
        NetworkManager.DisconnectService();
    }
}
