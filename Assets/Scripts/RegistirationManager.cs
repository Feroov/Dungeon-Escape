using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Text.RegularExpressions;

public class RegistrationManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private TMP_InputField passwordConfirmInput;

    [Header("Buttons")]
    [SerializeField] private Button registerButton;
    [SerializeField] private Button loginButton;

    [Header("Warning Objects")]
    [SerializeField] private GameObject passwordRequiredWarning;
    [SerializeField] private GameObject passwordsMatchWarning;
    [SerializeField] private GameObject usernameExistsWarning;
    [SerializeField] private GameObject successGameObject;
    [SerializeField] private GameObject invalidUsernameWarning;
    [SerializeField] private GameObject weakPasswordWarning;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip errorSound;
    [SerializeField] private AudioClip successSound;
    [SerializeField] private AudioClip buttonClickSound;
    
    private void Start()
    {
        registerButton.onClick.AddListener(OnRegisterButtonClick);
        passwordRequiredWarning.SetActive(false);
        loginButton.onClick.AddListener(OnLoginButtonClick);
        passwordsMatchWarning.SetActive(false);
        successGameObject.SetActive(false);
        usernameExistsWarning.SetActive(false);
        invalidUsernameWarning.SetActive(false);
        weakPasswordWarning.SetActive(false);
    }

    private void OnRegisterButtonClick()
    {
        audioSource.PlayOneShot(buttonClickSound);

        string username = usernameInput.text;
        string password = passwordInput.text;
        string passwordConfirm = passwordConfirmInput.text;

        // Basic client-side validation
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            Debug.LogError("Username and password are required");
            ShowWarning(passwordRequiredWarning);
            return;
        }

        // Validate username format (alphanumeric characters only)
        if (!Regex.IsMatch(username, "^[a-zA-Z0-9]*$"))
        {
            Debug.LogError("Username must contain only alphanumeric characters");
            ShowWarning(invalidUsernameWarning);
            return;
        }

        // Validate password length
        if (password.Length < 6)
        {
            Debug.LogError("Password must be at least 6 characters long");
            ShowWarning(weakPasswordWarning);
            return;
        }

        // Validate password complexity
        if (!Regex.IsMatch(password, "(?=.*[A-Z])(?=.*[^A-Za-z0-9]).{6,}"))
        {
            Debug.LogError("Password must contain at least one uppercase letter and one special character");
            ShowWarning(weakPasswordWarning);
            return;
        }

        // Check if password and confirm password match
        if (password != passwordConfirm)
        {
            Debug.LogError("Passwords do not match");
            ShowWarning(passwordsMatchWarning);
            return;
        }

        StartCoroutine(SendRegistrationRequest(username, password));
    }

    private IEnumerator SendRegistrationRequest(string username, string password)
    {
        string url = "https://dungeon-escape.000webhostapp.com/check_username.php?username=" + UnityWebRequest.EscapeURL(username);
        using (UnityWebRequest checkRequest = UnityWebRequest.Get(url))
        {
            yield return checkRequest.SendWebRequest();

            if (checkRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error checking username: " + checkRequest.error);
                yield break; // Exit the coroutine early
            }

            if (checkRequest.downloadHandler.text == "Username exists")
            {
                Debug.LogError("Username already exists");
                ShowWarning(usernameExistsWarning); // Display a warning indicating that the username already exists
                yield break; // Exit the coroutine early
            }
        }

        // If the username doesn't exist, proceed with registration
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        using (UnityWebRequest www = UnityWebRequest.Post("https://dungeon-escape.000webhostapp.com/register.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + www.error);
            }
            else
            {
                Debug.Log("Registration successful");
                ShowSuccess();
            }
        }
    }

    private void ShowWarning(GameObject warningGameObject)
    {
        warningGameObject.SetActive(true); // Show the warning GameObject
        audioSource.PlayOneShot(errorSound); // Play error sound
        StartCoroutine(HideAfterDelay(warningGameObject, 5f)); // Hide warning after 5 seconds
    }

    private void ShowSuccess()
    {
        successGameObject.SetActive(true); // Show the success GameObject
        audioSource.PlayOneShot(successSound); // Play success sound
        StartCoroutine(HideAfterDelay(successGameObject, 3f)); // Hide success after 3 seconds
        ResetInputFields(); // Reset input fields
    }

    private IEnumerator LoadLoginSceneWithSound()
    {
        // Play the button click sound
        audioSource.PlayOneShot(buttonClickSound);

        // Wait for the button click sound to finish playing
        yield return new WaitForSeconds(buttonClickSound.length);

        // Load the login scene
        SceneManager.LoadScene("Login");
    }

    private void OnLoginButtonClick()
    {
        StartCoroutine(LoadLoginSceneWithSound());
    }


    private void ResetInputFields()
    {
        usernameInput.text = ""; // Clear username input
        passwordInput.text = ""; // Clear password input
        passwordConfirmInput.text = ""; // Clear password confirm input
        usernameInput.placeholder.GetComponent<TextMeshProUGUI>().text = "Username"; // Reset placeholder text
        passwordInput.placeholder.GetComponent<TextMeshProUGUI>().text = "Password"; // Reset placeholder text
        passwordConfirmInput.placeholder.GetComponent<TextMeshProUGUI>().text = "Confirm Password"; // Reset placeholder text
    }

    private IEnumerator HideAfterDelay(GameObject gameObject, float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false); // Hide the GameObject after the delay
    }
}
