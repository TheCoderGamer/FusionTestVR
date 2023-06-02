using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using UnityEngine;

namespace Network
{
    public class NetworkMangerEvents : MonoBehaviour, INetworkRunnerCallbacks
    {
        public PlayerSpawner playerSpawner;

        public HardwareRig hardwareRig;


        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            playerSpawner.PlayerJoined(runner, player);
        }

        // Esto es llamado cada tick desde NetworkRig (FixedUpdateNetwork)
        public void OnInput(NetworkRunner runner, NetworkInput input)
        {
            // Esperar hasta que el jugador termine de spawnear
            if (hardwareRig != null)
            {
                hardwareRig.OnInput(runner, input);
            }
        }

        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
        {
            Debug.Log("Runner Shutdown: " + shutdownReason);
        }

        #region UnusedCallbacks
        
        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
        }
        
        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
        {
        }

        // ReSharper disable once Unity.IncorrectMethodSignature
        public void OnConnectedToServer(NetworkRunner runner)
        {
        }

        public void OnDisconnectedFromServer(NetworkRunner runner)
        {
        
        }

        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
        {
        
        }

        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
        {
        
        }

        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
        {
        
        }

        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
        {
        
        }

        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
        {
        
        }

        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
        {
        
        }

        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
        {
        
        }

        public void OnSceneLoadDone(NetworkRunner runner)
        {
        
        }

        public void OnSceneLoadStart(NetworkRunner runner)
        {
        
        }
        
        #endregion
    }
}
