using UnityEngine;

namespace Network
{
    public abstract class BaseNetworkService/* : INetworkServices*/
    {
        private NetworkStates _state = NetworkStates.Offline;

        public bool InLobby() => _state == NetworkStates.Lobby || _state == NetworkStates.Room;

        public abstract void Connect();

        public abstract void JoinLobby();
    }
}
