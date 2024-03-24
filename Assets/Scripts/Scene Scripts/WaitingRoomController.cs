using Mirror;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WaitingRoomController : MonoBehaviour
{
    [SerializeField] private Button startGameButton; // Assign this in the Unity Inspector

    // Start is called before the first frame update
    void Start()
    {
        // Only the host should have the start game button active.
        startGameButton.gameObject.SetActive(NetworkServer.active);

        if (NetworkServer.active)
        {
            startGameButton.onClick.AddListener(HostStartGame);
        }
    }

    private void HostStartGame()
    {
        // Change the scene for all connected clients to the game scene.
        // Ensure "GameSceneName" matches the name of your game scene.
        NetworkManager.singleton.ServerChangeScene("Game");
    }
}
