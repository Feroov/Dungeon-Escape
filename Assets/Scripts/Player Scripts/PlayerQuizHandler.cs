using Mirror;
using UnityEngine;

public class PlayerQuizHandler : NetworkBehaviour
{
    private QuizManager quizManager;

    [Command]
    public void CmdTriggerQuiz()
    {
        if (quizManager == null)
        {
            quizManager = FindObjectOfType<QuizManager>();
        }

        if (quizManager != null)
        {
            TargetOpenQuiz();
        }
    }

    [TargetRpc]
    private void TargetOpenQuiz()
    {
        // Make sure the quiz is only shown on the client that triggered it
        if (isLocalPlayer)
        {
            quizManager.StartRandomQuiz();
        }
    }
}
