using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public GameObject quizPanel; // Reference to the panel that contains the quiz UI
    public TextMeshProUGUI questionText; // Reference to the text UI element that will display the question
    public Button[] answerButtons; // References to the buttons for answers
    private string correctAnswer; // Store the correct answer

    public GameObject correctImage;
    public GameObject incorrectImage;
    public AudioClip correctSound;
    public AudioClip incorrectSound;
    private AudioSource audioSource;

    public Questions questionsScript;

    void Start()
    {
        quizPanel.SetActive(false);
        correctImage.SetActive(false);
        incorrectImage.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    public void StartRandomQuiz()
    {
        // Ensure there are questions available
        if (questionsScript.triviaQuestions.Count == 0)
        {
            Debug.LogError("No questions available.");
            return;
        }

        // Select a random question
        int index = Random.Range(0, questionsScript.triviaQuestions.Count);
        TriviaQuestion randomQuestion = questionsScript.triviaQuestions[index];

        // Start the quiz with the selected question
        StartQuiz(randomQuestion.question, randomQuestion.answers, randomQuestion.correctAnswerIndex);
    }


    // Call this method to start the quiz
    public void StartQuiz(string question, string[] answers, int correctAnswerIndex)
    {
        questionText.text = question;
        correctAnswer = answers[correctAnswerIndex];

        // Remove all existing listeners to avoid stacking them up on multiple quiz starts.
        foreach (var button in answerButtons)
        {
            button.onClick.RemoveAllListeners();
        }

        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i < answers.Length)
            {
                answerButtons[i].gameObject.SetActive(true);

                // Get the TextMeshProUGUI component instead of the Text component
                TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                buttonText.text = answers[i];

                int index = i; // Copy the current index to use inside the listener
                answerButtons[i].onClick.AddListener(() => AnswerSelected(answers[index]));
            }
            else
            {
                answerButtons[i].gameObject.SetActive(false);
            }

        }
        quizPanel.SetActive(true); // Show the quiz UI
        FindObjectOfType<FirstPersonLook>().SetCanLook(false);
    }


    private void AnswerSelected(string selectedAnswer)
    {
        // Disable the buttons to prevent multiple answers
        foreach (Button button in answerButtons)
        {
            button.interactable = false;
        }

        // Check if the answer is correct
        if (selectedAnswer == correctAnswer)
        {
            Debug.Log("Correct!");
            correctImage.SetActive(true);
            audioSource.PlayOneShot(correctSound);
            StartCoroutine(HideResultAndResetQuiz(correctImage));
        }
        else
        {
            Debug.Log("Incorrect.");
            incorrectImage.SetActive(true);
            audioSource.PlayOneShot(incorrectSound);
            StartCoroutine(HideResultAndResetQuiz(incorrectImage));
        }
    }
    IEnumerator HideResultAndResetQuiz(GameObject image)
    {
        // Wait for 2 seconds to show the result
        yield return new WaitForSeconds(1);

        // Hide the result image
        image.SetActive(false);

        // Hide the quiz panel
        quizPanel.SetActive(false);

        // Re-enable the buttons for the next time the quiz is started
        foreach (Button button in answerButtons)
        {
            button.interactable = true;
        }

        // Enable player controls again after showing result image
        FindObjectOfType<FirstPersonLook>().SetCanLook(true);
    }
}
