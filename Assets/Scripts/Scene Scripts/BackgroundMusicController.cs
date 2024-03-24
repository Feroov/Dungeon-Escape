using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusicController : MonoBehaviour
{
    private static BackgroundMusicController instance;

    private AudioSource audioSource;
    private bool shouldPlayMusic = false;

    private void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
        SceneManager.sceneLoaded += OnSceneLoaded;
        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }


    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded: " + scene.name);

        // Check if the current scene is one of the allowed scenes to play music
        if (scene.name == "Register" || scene.name == "Login" || scene.name == "Main")
        {
            shouldPlayMusic = true;
            Debug.Log("Playing music in scene: " + scene.name);
            PlayMusic();
        }
        else
        {
            shouldPlayMusic = false;
            Debug.Log("Stopping music because scene is: " + scene.name);
            StopMusic();
        }
    }

    private void Update()
    {
        if (shouldPlayMusic && !audioSource.isPlaying)
        {
            Debug.Log("Music stopped unexpectedly, restarting...");
            PlayMusic();
        }
        else if (!shouldPlayMusic && audioSource.isPlaying)
        {
            Debug.Log("Music should not be playing, stopping...");
            StopMusic();
        }
    }

    private void PlayMusic()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }


    private void StopMusic()
    {
        audioSource.Stop();
    }
}
