using Mirror;
using UnityEngine;

public class DoorQuiz : NetworkBehaviour
{
    public Questions questions; // This script should contain the questions data.

    void Start()
    {
        // Ensure that the Questions script is assigned.
        if (questions == null)
        {
            questions = FindObjectOfType<Questions>();
        }
    }

    [ServerCallback] // This ensures this code only runs on the server.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var playerNetworkIdentity = other.GetComponent<NetworkIdentity>();

            // Ensure that the object has a NetworkIdentity and that the client has authority over their player object.
            if (playerNetworkIdentity != null && playerNetworkIdentity.connectionToClient != null)
            {
                // Now trigger the quiz for the player who interacted with the door.
                TargetOpenQuiz(playerNetworkIdentity.connectionToClient);
            }
        }
    }

    [TargetRpc] // This will send an RPC to the specific client who interacted with the door.
    private void TargetOpenQuiz(NetworkConnection target)
    {
        // The client who has interacted with the door will now handle opening the quiz.
        // This should find the QuizManager in the client's local scene.
        var quizManager = FindObjectOfType<QuizManager>();
        if (quizManager != null)
        {
            quizManager.StartRandomQuiz(); // Assumes that QuizManager has this method.
        }
        else
        {
            Debug.LogError("QuizManager not found in the scene.");
        }
    }
}
