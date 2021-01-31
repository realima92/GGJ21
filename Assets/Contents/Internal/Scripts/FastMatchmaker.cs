using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastMatchmaker : MonoBehaviourPunCallbacks
{
    //public Text chatLog;
    const string SALA_NOME = "teste3";
    public bool waitOtherPlayer = true;

    private void Awake()
    {
        PhotonNetwork.LocalPlayer.NickName = "Eu#" + Random.Range(0, 1000000);
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        log("Conectado ao servidor!");
        if (PhotonNetwork.InLobby == false)
        {
            log("Entrando no lobby");
            PhotonNetwork.JoinLobby();
        }
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        log("Entrei no lobby!");
        PhotonNetwork.JoinRoom(SALA_NOME);

        //PhotonNetwork.JoinOrCreateRoom("teste");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        log("Não deu Join: #" + returnCode + ": " + message);
        if (returnCode == ErrorCode.GameDoesNotExist)
        {
            log("Criando sala " + SALA_NOME + "...");

            RoomOptions room = new RoomOptions { MaxPlayers = 2 };
            PhotonNetwork.CreateRoom(SALA_NOME, room);
        }
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        log("Você entrou na sala! Seu usuário é " + PhotonNetwork.LocalPlayer.NickName);
        //GameState.Start();
        //SceneManager.LoadScene("Scenes/Cena2");
        if(PhotonNetwork.LocalPlayer.ActorNumber == 2 || !waitOtherPlayer)
        {
            log("Player #" + PhotonNetwork.LocalPlayer.ActorNumber + ": Criando jogador");
            GameManager.Instance.CreatePlayer();
        }
    }

    public override void OnLeftRoom()
    {
        log("Saiu da sala");
        base.OnLeftRoom();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        if (waitOtherPlayer)
        {
            log("Novo jogador entrou, criando seu jogador agora");
            GameManager.Instance.CreatePlayer();
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        log("Jogador saiu da sala");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void log(string str)
    {
        Debug.Log(str);
        //chatLog.text += "\n" + str;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
