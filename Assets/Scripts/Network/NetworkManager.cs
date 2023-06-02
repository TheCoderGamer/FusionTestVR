using System.Threading.Tasks;
using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Network
{
    public class NetworkManager : MonoBehaviour
    {
    
        // -- Variables --
        [Header("Settings")]
        [SerializeField] private GameObject networkRunnerPrefab;
        [SerializeField] private NetworkPrefabRef playerSpawnerPrefab;
        
        
        // Singleton (Para Nacho: Esto es una forma de hacer una "variable global" para un singleton que solo pueda settearse desde dentro de la clase)
        public static NetworkManager Instance { get; private set; }
        private NetworkRunner SessionRunner { get; set; }
        private NetworkMangerEvents _networkMangerEvents;


        private void Awake()
        {
            // -- Singleton de NetworkManager --
            if (Instance != null && Instance != this)
            {
                Debug.LogError("There is more than one NetworkManager in the scene!");
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        
            _networkMangerEvents = GetComponent<NetworkMangerEvents>();
        }
        
        // Llamar a esta funcion desde un boton
        public async void StartSharedSession()
        {
            CreateRunner();

            await LoadScene();
                            
            await Connect();
        }

        private async Task LoadScene()
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);

            while (!asyncLoad.isDone)
            {
                await Task.Yield();
            }
        }

        private void CreateRunner()
        {
            // Instanciar el runner y asignarle los callbacks
            SessionRunner = Instantiate(networkRunnerPrefab).GetComponent<NetworkRunner>();
            DontDestroyOnLoad(SessionRunner);
            SessionRunner.AddCallbacks(_networkMangerEvents);
        
            // - Asignar variables -

            // PlayerSpawner
            PlayerSpawner playerSpawner = SessionRunner.GetComponent<PlayerSpawner>();
            _networkMangerEvents.playerSpawner = playerSpawner;
            
            // PlayerPrefab
            playerSpawner.playerPrefab = playerSpawnerPrefab;
        }

        public void AssignHardwareRig(HardwareRig hardwareRig)
        {
            _networkMangerEvents.hardwareRig = hardwareRig;
        }

        private async Task Connect()
        {
            // Crear sala
            var args = new StartGameArgs
            {
                GameMode = GameMode.Shared,
                SessionName = "TestSession",
                SceneManager = GetComponent<NetworkSceneManagerDefault>()
            };
        
            // Iniciar sala
            var result = await SessionRunner.StartGame(args);
        
            // Verificar sala
            if (result.Ok)
            {
                Debug.Log("Connected to server");
            }
            else
            {
                Debug.Log("Failed to connect to server");
                Debug.Log(result.ErrorMessage);
            }
        }
    }
}
