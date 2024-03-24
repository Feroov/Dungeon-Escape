using UnityEngine;
using TMPro;

public class UsernameDisplay : MonoBehaviour
{
    public TextMeshPro usernameText;
    public PlayerData playerData; // Reference to the PlayerData component

    void Start()
    {
        if (playerData != null)
        {
            UpdateUsernameDisplay(playerData.playerName);
        }
    }

    public void UpdateUsernameDisplay(string name)
    {
        // Update the TextMeshPro text to show the username
        if (usernameText != null)
        {
            usernameText.text = name;
        }
    }
}
