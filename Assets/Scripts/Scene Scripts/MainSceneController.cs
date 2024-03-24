using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro; // Include this if you're using TextMeshPro for the input field
using Mirror;

public class MainSceneController : MonoBehaviour
{
    [SerializeField] private Button hostButton;
    [SerializeField] private Button joinButton;
    [SerializeField] private TMP_InputField ipAddressInputField; // Reference to your IP address input field

    private void Start()
    {
        if (hostButton != null)
        {
            hostButton.onClick.AddListener(OnHostButtonClick);
        }

        if (joinButton != null)
        {
            joinButton.onClick.AddListener(OnJoinButtonClick);
        }
    }

    public void OnHostButtonClick()
    {
        NetworkManager.singleton.StartHost();
        SceneManager.LoadScene("WaitingRoom");
    }

    public void OnJoinButtonClick()
    {
        string ipAddress = ipAddressInputField.text.Trim();
        if (string.IsNullOrEmpty(ipAddress))
        {
            Debug.Log("IP address is empty. Please enter a valid IP address to join.");
            return; // Exit the method if no address is provided
        }

        if (PlayerPrefs.HasKey("LoggedInUser"))
        {
            NetworkManager.singleton.networkAddress = ipAddress;
            NetworkManager.singleton.StartClient();
            SceneManager.LoadScene("WaitingRoom");
        }
        else
        {
            Debug.LogWarning("No user is logged in. Please log in first.");
        }
    }

}
