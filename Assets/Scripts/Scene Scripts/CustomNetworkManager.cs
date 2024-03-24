using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class CustomNetworkManager : NetworkManager
{
    public GameObject chatPlayerPrefab;
    public GameObject firstPersonPlayerPrefab;

    public override void OnClientConnect()
    {
        base.OnClientConnect();

        if (autoCreatePlayer)
        {
            // Call AddPlayer on the client connection
            NetworkClient.AddPlayer();
        }
    }


    public override void OnServerSceneChanged(string sceneName)
    {
        if (sceneName == "Game")
        {
            // Change to the game player prefab
            playerPrefab = firstPersonPlayerPrefab;
        }
    }
}
