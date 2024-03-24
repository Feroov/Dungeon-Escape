using UnityEngine;
using TMPro;

public class Main : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI valueText;

    private void Start()
    {
        // Check if the player is logged in
        if (PlayerPrefs.HasKey("LoggedInUser"))
        {
            // Get the logged-in user from PlayerPrefs
            string loggedInUser = PlayerPrefs.GetString("LoggedInUser");
            
            // Display the logged-in user on the UI
            valueText.text = loggedInUser;
        }
        else
        {
            // If no user is logged in, clear the text
            valueText.text = "";
        }
    }
}
