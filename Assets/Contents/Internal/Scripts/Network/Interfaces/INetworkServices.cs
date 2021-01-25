namespace Network
{
    public interface INetworkServices
    {
        /// <summary>
        /// Connect to service
        /// </summary>
        void OnEnable();
        /// <summary>
        /// Disconnect to service
        /// </summary>
        void OnDisable();

        void JoinLobby();
        bool InLobby();
    }
}
