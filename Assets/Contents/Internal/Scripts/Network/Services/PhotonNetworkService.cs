using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;

namespace Network
{
    public class PhotonNetworkService : MonoBehaviourPunCallbacks, INetworkServices
    {

        private NetworkStates _state = NetworkStates.Offline;

        public bool InLobby() => _state == NetworkStates.Lobby || _state == NetworkStates.Room;

        public delegate void ConnectionTimeoutEvent();
        public ConnectionTimeoutEvent connectionTimeoutEvent;
        public delegate void LobbyConnectedEvent();
        public LobbyConnectedEvent lobbyConnectedEvent;
        public delegate void RoomNotFoundEvent(string name);
        public RoomNotFoundEvent roomNotFoundEvent;
        public delegate void RoomEnteredEvent();
        public RoomEnteredEvent roomEnteredEvent;
        public delegate void RoomStartedEvent();
        public RoomStartedEvent roomStartedEvent;

        private string roomName;


        #region Start Connection

        public override void OnEnable()
        {
            base.OnEnable();

            Debug.Log($"[Network][{this.GetType().Name}] Connecting to server...");
            //Player Nickname. Link with playfab?
            PhotonNetwork.LocalPlayer.NickName = "RndNick_" + Random.Range(1, 9999).ToString("0000");
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnDisable()
        {
            base.OnDisable();
            PhotonNetwork.Disconnect();
            _state = NetworkStates.Offline;

            Debug.Log($"[Network][{this.GetType().Name}] Disconnected from server. Cause: Component Disabled");
        }

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();
            _state = NetworkStates.Connected;
            Debug.Log($"[Network][{this.GetType().Name}] Connected as: {PhotonNetwork.LocalPlayer.NickName}");

            JoinLobby();
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            base.OnDisconnected(cause);
            _state = NetworkStates.Offline;

            if(cause == DisconnectCause.ClientTimeout)
            {
                connectionTimeoutEvent();
            }

            Debug.LogWarning($"[Network][{this.GetType().Name}] Disconnected from server. Cause: {cause}");
        }

        #endregion

        #region Lobby
        public void JoinLobby()
        {
            if (PhotonNetwork.InLobby)
            {
                Debug.LogWarning($"[Network][{this.GetType().Name}] Already in lobby.");
            }
            else if (_state == NetworkStates.Connected)
            {
                Debug.Log($"[Network][{this.GetType().Name}] Joining lobby...");
                PhotonNetwork.JoinLobby();
            }
            else
            {
                Debug.LogError($"[Network][{this.GetType().Name}] Cannot join lobby. Connection state: {_state}");
            }
        }

        public override void OnJoinedLobby()
        {
            base.OnJoinedLobby();
            _state = NetworkStates.Lobby;
            lobbyConnectedEvent();
            Debug.Log($"[Network][{this.GetType().Name}] Joined lobby");
        }
        #endregion

        #region Room
        public void JoinRoom(string name)
        {
            roomName = name;
            Debug.Log($"[Network][{this.GetType().Name}] Joining room: { (string.IsNullOrEmpty(roomName) ? "Random" : roomName) }");
            if (string.IsNullOrEmpty(roomName))
            {
                //Random Room
                PhotonNetwork.JoinRandomRoom(null, NetworkManager.Instance.maxPlayers, MatchmakingMode.FillRoom, TypedLobby.Default, null);
                StartCoroutine("Timeout");
            }
            else
            {
                //Specific Room
                PhotonNetwork.JoinRoom(roomName);
            }
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            _state = NetworkStates.Room;
            Debug.Log($"[Network][{this.GetType().Name}] Joined room! Room: {PhotonNetwork.CurrentRoom.Name}");
            //roomEnteredEvent(PhotonNetwork.CurrentRoom.);
            if(PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                //Iniciar a partida
                roomStartedEvent();
            }
        }

        public void CreateRoom(string name)
        {
            if (string.IsNullOrEmpty(name))
            { 
                roomName = $"RndRoom-{ Random.Range(0, 9999).ToString("0000") }";
            }
            else
            {
                roomName = name;
            }
            Debug.Log($"[Network][{this.GetType().Name}] Creating room ! Room: {roomName}");
            var options = new RoomOptions() { MaxPlayers = 2, IsVisible = string.IsNullOrEmpty(name)};
            PhotonNetwork.CreateRoom(roomName, options);
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
            Debug.Log($"[Network][{this.GetType().Name}] {newPlayer.NickName} entered room!");

            if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                //Iniciar a partida
                roomStartedEvent();
            }
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            base.OnPlayerLeftRoom(otherPlayer);
            Debug.Log($"[Network][{this.GetType().Name}] {otherPlayer.NickName} left room!");
        }

        #region Error Handles
        public IEnumerator Timeout()
        {
            yield return new WaitForSeconds(NetworkManager.Instance.timeout);
            HandleRoomNotFound(roomName);
        }

        public void HandleRoomNotFound(string name)
        {
            Debug.Log($"[Network][{this.GetType().Name}] Joining room Alert! Room not found");
            roomNotFoundEvent(name);
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            base.OnJoinRoomFailed(returnCode, message);

            if (returnCode == ErrorCode.GameDoesNotExist)
            {
                //Alert Player
                HandleRoomNotFound(name);
            }
            else
            {
                Debug.LogError($"[Network][{this.GetType().Name}] Joining room Error! {message}");
            }
        }

        public override void OnErrorInfo(ErrorInfo errorInfo)
        {
            Debug.LogError($"[Network][{this.GetType().Name}] Error! {errorInfo.Info}");
        }
        #endregion
        #endregion
    }
}
