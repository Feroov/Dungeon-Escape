using UnityEngine;

public class DoorQuiz : MonoBehaviour
{
    public QuizManager quizManager; // Reference to the QuizManager in the scene
    public Questions questions;

    void Start()
    {
        // Make sure the questions script is assigned
        if (questions == null)
        {
            questions = FindObjectOfType<Questions>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Make sure the player is tagged correctly
        {
            // Get a random question from the list
            TriviaQuestion randomQuestion = questions.GetRandomQuestion();
            if (randomQuestion != null)
            {
                // Start the quiz with the random question
                quizManager.StartQuiz(randomQuestion.question, randomQuestion.answers, randomQuestion.correctAnswerIndex);
            }
        }
    }
}
