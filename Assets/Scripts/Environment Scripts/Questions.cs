using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TriviaQuestion
{
    public string question;
    public string[] answers;
    public int correctAnswerIndex;
}
public class Questions : MonoBehaviour
{
    public List<TriviaQuestion> triviaQuestions = new List<TriviaQuestion>();

    void Start()
    {
        // Populate your list with trivia questions here
        // For example purposes, I'm adding only a few
        triviaQuestions.Add(new TriviaQuestion
        {
            question = "What is the capital of France?",
            answers = new[] { "Berlin", "Paris", "Madrid" },
            correctAnswerIndex = 1
        });

        // Add more questions similarly...
    }

    // Method to get a random question
    public TriviaQuestion GetRandomQuestion()
    {
        if (triviaQuestions.Count == 0) return null;
        return triviaQuestions[Random.Range(0, triviaQuestions.Count)];
    }
}
