using UnityEngine;

namespace Network
{
    public class NetworkManager : MonoBehaviour
    {
        private static NetworkManager _instance;
        private static INetworkServices _service;

        public static NetworkManager Instance => _instance;

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
            _service = _instance.gameObject.AddComponent<PhotonNetworkService>();
        }

        public static void DisconnectService()
        {
            _service = null;
            Destroy((Component) _instance.gameObject.GetComponent<INetworkServices>());
        }


    }
}
