using Mirror;
using TMPro; // Import the TextMeshPro namespace
using UnityEngine;

public class PlayerData : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnNameChanged))]
    public string playerName;

    public TextMeshPro usernameText; // Add this field to hold a reference to the TextMeshPro component

    public override void OnStartServer()
    {
        base.OnStartServer();

        // Generate a unique name for illustration purposes.
        // In an actual game, you would have a different mechanism for setting names.
        playerName = "Player" + Random.Range(1, 100);
    }

    public override void OnStartClient()
    {
        // Call this on all clients to ensure the initial name is set correctly from the start.
        usernameText.text = playerName;
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        // Set your local player's name, but only if it's the local player
        if (isLocalPlayer)
        {
            string loggedInUser = PlayerPrefs.HasKey("LoggedInUser") ? PlayerPrefs.GetString("LoggedInUser") : "Guest";
            CmdSetPlayerName(loggedInUser);
        }

        // Also, disable the text display if it's the local player to avoid seeing our own label
        usernameText.gameObject.SetActive(false);
    }

    private void OnNameChanged(string oldValue, string newValue)
    {
        // Update the display name whenever the playerName changes on the server
        usernameText.text = newValue;
    }

    [Command]
    private void CmdSetPlayerName(string name)
    {
        playerName = name;
    }
}
