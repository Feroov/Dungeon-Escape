using UnityEngine;
using Mirror;
using TMPro;

public class ChatManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField chatInputField;
    [SerializeField] private TextMeshProUGUI chatDisplay;

    private void Start()
    {
        // Display a join message locally for the player when they start/connect
        string loggedInUser = PlayerPrefs.GetString("LoggedInUser", "Anonymous");
        DisplayLocalSystemMessage($"{loggedInUser} has joined.");
    }

    private void Awake()
    {
        // Register handlers
        if (NetworkServer.active)
        {
            NetworkServer.RegisterHandler<ChatMessage>(OnServerReceivedMessage);
        }
        NetworkClient.RegisterHandler<ChatMessage>(OnClientReceivedMessage);
    }

    public void SendChatMessage()
    {
        if (chatInputField.text.Trim() == "")
        {
            return;
        }

        ChatMessage chatMessage = new ChatMessage
        {
            sender = PlayerPrefs.GetString("LoggedInUser", "Anonymous"),
            message = chatInputField.text
        };

        NetworkClient.Send(chatMessage);
        chatInputField.text = string.Empty;
        chatInputField.ActivateInputField();
    }

    private void OnServerReceivedMessage(NetworkConnection conn, ChatMessage chatMessage)
    {
        NetworkServer.SendToAll(chatMessage);
    }

    private void OnClientReceivedMessage(ChatMessage chatMessage)
    {
        // Determine if the sender is the host
        bool isHost = chatMessage.sender == PlayerPrefs.GetString("HostUsername", "Host"); // Example condition

        // Apply a different color to the host's name
        string formattedMessage = isHost ? $"<color=#FF0000>{chatMessage.sender}</color>: {chatMessage.message}\n" :
                                           $"{chatMessage.sender}: {chatMessage.message}\n";

        chatDisplay.text += formattedMessage;
    }


    private void DisplayLocalSystemMessage(string message)
    {
        // This method displays system messages locally, such as join/leave messages
        chatDisplay.text += $"System: {message}\n";
    }

    private void OnDestroy()
    {
        // Possible place to display a disconnect message locally
        string loggedInUser = PlayerPrefs.GetString("LoggedInUser", "Anonymous");
        DisplayLocalSystemMessage($"{loggedInUser} has left.");
    }
}
