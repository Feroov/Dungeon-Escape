using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class GameManager : MonoBehaviour
{
    public GameObject chatPlayerPrefab; // Assign in inspector
    public GameObject firstPersonPlayerPrefab; // Assign in inspector

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Game")
        {
            NetworkManager.singleton.playerPrefab = firstPersonPlayerPrefab;
        }
        else if (scene.name == "WaitingRoom")
        {
            NetworkManager.singleton.playerPrefab = chatPlayerPrefab;
        }
    }
}
