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
        triviaQuestions = new List<TriviaQuestion>
    {
        new TriviaQuestion {
            question = "What's the largest planet in our solar system?",
            answers = new[] { "Jupiter", "Saturn", "Mars" },
            correctAnswerIndex = 0
        },
        new TriviaQuestion {
            question = "Where are the Pyramids of Giza?",
            answers = new[] { "India", "Egypt", "Mexico" },
            correctAnswerIndex = 1
        },
        new TriviaQuestion {
            question = "Which element does 'O' represent on the periodic table?",
            answers = new[] { "Gold", "Oxygen", "Silver" },
            correctAnswerIndex = 1
        },
        new TriviaQuestion {
            question = "Who painted the Mona Lisa?",
            answers = new[] { "Van Gogh", "Da Vinci", "Picasso" },
            correctAnswerIndex = 1
        },
        new TriviaQuestion {
            question = "What's the capital of Italy?",
            answers = new[] { "Rome", "Paris", "Berlin" },
            correctAnswerIndex = 0
        },
        new TriviaQuestion {
            question = "How many continents are there?",
            answers = new[] { "5", "6", "7" },
            correctAnswerIndex = 2
        },
        new TriviaQuestion {
            question = "Who wrote 'Romeo and Juliet'?",
            answers = new[] { "Shakespeare", "Hemingway", "Austen" },
            correctAnswerIndex = 0
        },
        new TriviaQuestion {
            question = "What's the hardest natural substance?",
            answers = new[] { "Gold", "Diamond", "Iron" },
            correctAnswerIndex = 1
        },
        new TriviaQuestion {
            question = "What does the Internet prefix WWW stand for?",
            answers = new[] { "World Wide Web", "Wide Web World", "Web World Wide" },
            correctAnswerIndex = 0
        },
        new TriviaQuestion {
            question = "Which gas do plants absorb from the atmosphere?",
            answers = new[] { "Oxygen", "Carbon Dioxide", "Nitrogen" },
            correctAnswerIndex = 1
        },
        new TriviaQuestion {
            question = "What's the largest ocean on Earth?",
            answers = new[] { "Atlantic", "Indian", "Pacific" },
            correctAnswerIndex = 2
        },
        new TriviaQuestion {
            question = "Who invented the telephone?",
            answers = new[] { "Edison", "Bell", "Tesla" },
            correctAnswerIndex = 1
        },
        new TriviaQuestion {
            question = "What does DNA stand for?",
            answers = new[] { "Deoxyribonucleic Acid", "Dinucleic Acid", "Acid Deoxyribo" },
            correctAnswerIndex = 0
        },
        new TriviaQuestion {
            question = "What year did the Titanic sink?",
            answers = new[] { "1912", "1911", "1913" },
            correctAnswerIndex = 0
        },
        new TriviaQuestion {
            question = "Which planet is closest to the Sun?",
            answers = new[] { "Venus", "Mercury", "Mars" },
            correctAnswerIndex = 1
        }
    };
    }

    // Method to get a random question
    public TriviaQuestion GetRandomQuestion()
    {
        if (triviaQuestions.Count == 0) return null;
        return triviaQuestions[Random.Range(0, triviaQuestions.Count)];
    }
}
