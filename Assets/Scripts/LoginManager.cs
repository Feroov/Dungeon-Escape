using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoginManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private Button loginButton;
    [SerializeField] private Button registerButton;
    [SerializeField] private GameObject loginFailedMessage;
    [SerializeField] private GameObject loginSuccessMessage;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip errorSound;
    [SerializeField] private AudioClip successSound;
    [SerializeField] private AudioClip buttonClickSound;

    private void Start()
    {
        loginButton.onClick.AddListener(OnLoginButtonClick);
        registerButton.onClick.AddListener(OnRegisterButtonClick);
        loginFailedMessage.SetActive(false);
        loginSuccessMessage.SetActive(false);
    }

    private void OnLoginButtonClick()
    {
        audioSource.PlayOneShot(buttonClickSound);

        string username = usernameInput.text;
        string password = passwordInput.text;

        // Basic client-side validation
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            Debug.LogError("Username and password are required");
            return;
        }

        StartCoroutine(SendLoginRequest(username, password));
    }

    private IEnumerator LoadRegisterSceneWithSound()
    {
        audioSource.PlayOneShot(buttonClickSound);

        yield return new WaitForSeconds(buttonClickSound.length);

        // Load the login scene
        SceneManager.LoadScene("Register");
    }

    private void OnRegisterButtonClick()
    {
        StartCoroutine(LoadRegisterSceneWithSound());
    }

    private IEnumerator SendLoginRequest(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        using (UnityWebRequest www = UnityWebRequest.Post("https://dungeon-escape.000webhostapp.com/login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + www.error);
            }
            else
            {
                string responseText = www.downloadHandler.text;

                if (responseText.Contains("Login successful"))
                {
                    Debug.Log("Login successful");
                    PlayerPrefs.SetString("LoggedInUser", username);
                    loginSuccessMessage.SetActive(true);
                    audioSource.PlayOneShot(successSound);
                    StartCoroutine(HideAndLoadAfterDelay(loginSuccessMessage, 1.5f, "Main"));
                }
                else
                {
                    Debug.LogError("Login failed");
                    loginFailedMessage.SetActive(true);
                    audioSource.PlayOneShot(errorSound);
                    StartCoroutine(HideAfterDelay(loginFailedMessage, 3f));
                }
            }
        }
    }


    private IEnumerator HideAfterDelay(GameObject gameObject, float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }

    private IEnumerator HideAndLoadAfterDelay(GameObject gameObject, float delay, string sceneName)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
        yield return new WaitForSeconds(0f);
        SceneManager.LoadScene(sceneName);
    }
}
