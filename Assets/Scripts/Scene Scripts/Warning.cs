using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Warning : MonoBehaviour
{
    public TMP_InputField inputField;
    public GameObject warningParent;
    public AudioClip warningSound; // Assign your audio clip in the Unity Inspector

    private AudioSource audioSource;

    private void Start()
    {
        // By default, make the warning parent object invisible
        warningParent.SetActive(false);

        // Add a listener to the input field to detect changes in text
        inputField.onValueChanged.AddListener(OnInputValueChanged);

        // Get the AudioSource component from the GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // If there's no AudioSource component attached to this GameObject, add one
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnInputValueChanged(string text)
    {
        // Check if the length of the input text is greater than or equal to 10
        if (text.Length >= 10)
        {
            // If it is, make the warning parent object visible
            warningParent.SetActive(true);

            // Play the warning sound
            if (warningSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(warningSound);
            }
        }
        else
        {
            // If not, make the warning parent object invisible
            warningParent.SetActive(false);
        }
    }
}
