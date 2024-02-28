using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using System.Collections;

public class RegistrationManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private TMP_InputField passwordConfirmInput;
    [SerializeField] private Button registerButton;
    [SerializeField] private GameObject passwordRequiredWarning;
    [SerializeField] private GameObject passwordsMatchWarning;
    [SerializeField] private GameObject successGameObject;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip errorSound;
    [SerializeField] private AudioClip successSound;

    private void Start()
    {
        registerButton.onClick.AddListener(OnRegisterButtonClick);
        passwordRequiredWarning.SetActive(false); // Initially hide the password required warning GameObject
        passwordsMatchWarning.SetActive(false); // Initially hide the passwords match warning GameObject
        successGameObject.SetActive(false); // Initially hide the success GameObject
    }

    private void OnRegisterButtonClick()
    {
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
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/register.php", form))
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
