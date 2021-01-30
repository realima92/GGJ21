using System;
using UnityEngine;

namespace Network
{
    public class NetworkManager : MonoBehaviour
    {
        private static NetworkManager _instance;
        private PhotonNetworkService _service;

        public static NetworkManager Instance => _instance;
        public static PhotonNetworkService Service { get { return _instance._service; } set { _instance._service = value; } }

        // Start is called before the first frame update
        void Awake()
        {
            //Singleton
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);

                //Test
                //ConnectService();
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        public static void ConnectService()
        {
            Service = _instance.gameObject.AddComponent<PhotonNetworkService>();
        }

        public static void DisconnectService()
        {
            Service = null;
            Destroy((Component) _instance.gameObject.GetComponent<PhotonNetworkService>());
        }

        public static void JoinRoom()
        {
            if (Service.InLobby())
            {
                Service.JoinRoom();
            }
            else
            {
                ConnectService();
            }
        }
    }
}
