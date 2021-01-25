using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace Network
{
    public class PhotonNetworkService : MonoBehaviourPunCallbacks, INetworkServices
    {
        private NetworkStates _state = NetworkStates.Offline;

        public bool InLobby() => _state == NetworkStates.Lobby || _state == NetworkStates.Room;

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

            Debug.LogWarning($"[Network][{this.GetType().Name}] Disconnected from server. Cause: {cause}");
        }

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
            Debug.Log($"[Network][{this.GetType().Name}] Joined lobby");
        }
    }
}
