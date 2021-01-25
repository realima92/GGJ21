using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace Network
{
    public class NetworkManager : MonoBehaviour
    {
        private static NetworkManager _manager;
        private INetworkServices _service;

        public static NetworkManager Manager => _manager;

        // Start is called before the first frame update
        void Awake()
        {
            //Singleton
            if (_manager == null)
            {
                _manager = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (_manager != this)
            {
                Destroy(gameObject);
            }

            //Test
            ConnectService();
        }

        public void ConnectService()
        {
            _service = gameObject.AddComponent<PhotonNetworkService>();
        }

        public void DisconnectService()
        {
            _service = null;
            Destroy((Component)gameObject.GetComponent<INetworkServices>());
        }
    }
}
