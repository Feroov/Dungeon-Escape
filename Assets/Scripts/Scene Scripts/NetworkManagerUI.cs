using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.SceneManagement;

public class NetworkManagerUI : MonoBehaviour
{
    [Header("UI")]
    public TMP_InputField joinGameInputField; // The input field for the IP address
    public GameObject mainMenuPanel; // The UI Panel that contains the buttons

    void Awake()
    {
        // This will prevent the GameObject that this script is attached to from being destroyed when loading a new scene
        DontDestroyOnLoad(this.gameObject);
    }
    
    public void HostGame()
    {
        // Start hosting the game
        NetworkManager.singleton.StartHost();

        // Mark this player as the host
        PlayerPrefs.SetString("HostUsername", PlayerPrefs.GetString("LoggedInUser", "Host"));

        // Optionally deactivate the main menu panel
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(false);
    }

    public void JoinGame()
    {
        // Set the network address to the IP address from the input field
        NetworkManager.singleton.networkAddress = joinGameInputField.text;

        // Start the client
        NetworkManager.singleton.StartClient();
        // SceneManager.LoadScene("WaitingRoom");

        // Optionally deactivate the main menu panel
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(false);
    }
}
