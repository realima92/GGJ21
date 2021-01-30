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
            Debug.Log($"[Network][{this.GetType().Name}] Joining room: { (string.IsNullOrEmpty(name) ? "Random" : name) }");
            if (string.IsNullOrEmpty(name))
            {
                //Random Room
                PhotonNetwork.JoinRandomRoom(null, NetworkManager.Instance.maxPlayers, MatchmakingMode.FillRoom, TypedLobby.Default, null);
                StartCoroutine("Timeout", "");
            }
            else
            {
                //Specific Room
                PhotonNetwork.JoinRoom(name);
                StartCoroutine("Timeout", name);
            }
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            _state = NetworkStates.Room;
            Debug.Log($"[Network][{this.GetType().Name}] Joined room! Room: {PhotonNetwork.CurrentRoom.Name}");
            roomEnteredEvent();
        }

        public void CreateRoom(string name)
        {
            if (string.IsNullOrEmpty(name)) name = $"Created room! Room: RndRoom-{ Random.Range(0,9999).ToString("0000") }";
            Debug.Log($"[Network][{this.GetType().Name}] Creating room ! Room: {name}");
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
            Debug.Log($"[Network][{this.GetType().Name}] {newPlayer.NickName} entered room!");
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            base.OnPlayerLeftRoom(otherPlayer);
            Debug.Log($"[Network][{this.GetType().Name}] {otherPlayer.NickName} left room!");
        }

        #region Error Handles
        public IEnumerator Timeout(string name = null)
        {
            yield return new WaitForSeconds(NetworkManager.Instance.timeout);
            HandleRoomNotFound(name);
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
