using UnityEngine;
using TMPro;
using Networking;
using System;
using System.Collections;


public class QuizDataSetter : MonoBehaviour
{
    [Header("Panels")]
    public GameObject quizSelectContainer;
    public GameObject quiz;
    public GameObject endScreen;
    [Header("Inputs")]
    public TextMeshProUGUI question;
    public TextMeshProUGUI ansA;
    public TextMeshProUGUI ansB;
    public TextMeshProUGUI ansC;
    public TextMeshProUGUI ansD;
    public TextMeshProUGUI progressText;
    public TextMeshProUGUI pointText;

    [Header("Others")]
    public GetDataGS getQuizData;
    public ScoreCounter score;
    private string[,] quizData;

    [Header("End Screen")]
    public GameObject[] endScreenStars;
    public TextMeshProUGUI endScoreText;
    public TextMeshProUGUI questionAnsText;

    private int totalQuestion;
    private int correctAns;
    private int displayingQuesion;
    private int questionIndex = 1;

    private void Awake()
    {
        getQuizData.OnOutputDone.AddListener(SetQuizData);
    }

    private void OnEnable()
    {
        quizSelectContainer.SetActive(true);
        quiz.SetActive(false);

        getQuizData.GetData(1, 0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DisplayQuestion(questionIndex++);
        }
    }

    public void SetQuizData()
    {
        quizData = getQuizData.datas;

        totalQuestion = quizData.GetLength(0) - 1;
        progressText.text = string.Format("{0} / {1}", questionIndex, totalQuestion);
        pointText.text = "0 pts";
    }

    public void SelectQuiz()
    {
        if (quizData == null) return;
        endScreen.SetActive(false);
        quizSelectContainer.SetActive(false);
        quiz.SetActive(true);

        ResetValue();

        score.StartTime();
        DisplayQuestion(questionIndex++);
    }

    public void SelectAnswer(string ans)
    {
        string correctAns = quizData[displayingQuesion, 5].Trim();
        correctAns.Replace(" ", "");
        if (correctAns.Equals(ans))
        {
            // Run if answer is correct
            CorrectAnswer();
        }
        else
        {
            // Run if answer is wrong
            WrongAnswer();
            score.WrongAns();
        }
    }

    public void CorrectAnswer()
    {
        //print("Correct");
        correctAns++;
        DisplayQuestion(questionIndex++);
        score.CorrectAns();
        pointText.text = score.currentScore.ToString("## pts");
    }

    public void WrongAnswer()
    {
        //print("Wrong");
        DisplayQuestion(questionIndex++);
    }

    public void EndQuiz()
    {
        endScreen.SetActive(true);

        // 1 Star (50%)
        if (score.currentScore/score.maxScore * 100 >= 35)
        {
            endScreenStars[0].SetActive(true);
        }

        // 2 star (65%)
        if (score.currentScore / score.maxScore * 100 >= 50)
        {
            endScreenStars[1].SetActive(true);
        }

        // 3 star (80%)
        if (score.currentScore / score.maxScore * 100 >= 60)
        {
            endScreenStars[2].SetActive(true);
        }

        endScoreText.text = score.currentScore.ToString("#");
        questionAnsText.text = String.Format("{0} / {1}", correctAns, totalQuestion);

    }

    public void Retry()
    {
        ResetValue();

        if (quizData == null) return;
        endScreen.SetActive(false);
        quizSelectContainer.SetActive(false);
        quiz.SetActive(true);

        score.StartTime();
        DisplayQuestion(questionIndex++);
    }

    public void ReturnToMenu()
    {
        ResetValue();

        if (quizData == null) return;
        endScreen.SetActive(false);
        quizSelectContainer.SetActive(true);
        quiz.SetActive(false);
    }

    public void DisplayQuestion(int questionIndex)
    {
        if (questionIndex > totalQuestion)
        {
            EndQuiz();
            return;
        }

        displayingQuesion = questionIndex;
        //int questionNumber = UnityEngine.Random.Range(1, quizData.GetLength(0));

        progressText.text = string.Format("{0} / {1}", questionIndex - 1, totalQuestion);
        StopAllCoroutines();
        StartCoroutine(TypeSentence(question, quizData[questionIndex, 0]));
        StartCoroutine(TypeSentence(ansA, quizData[questionIndex, 1]));
        StartCoroutine(TypeSentence(ansB, quizData[questionIndex, 2]));
        StartCoroutine(TypeSentence(ansC, quizData[questionIndex, 3]));
        StartCoroutine(TypeSentence(ansD, quizData[questionIndex, 4]));
    }

    IEnumerator TypeSentence(TextMeshProUGUI target, string sentense)
    {
        target.text = "";
        foreach (char letter in sentense.ToCharArray())
        {
            target.text += letter;
            yield return null;
        }
    }

    private void ResetValue()
    {
        ShuffleArray(quizData);
        questionIndex = 1;
        correctAns = 0;
        displayingQuesion = 0;
        score.ClearScore();
        pointText.text = "0 pts";
        progressText.text = string.Format("{0} / {1}", questionIndex, totalQuestion);
    }

    private void ShuffleArray(string[,] arr)
    {
        int lengthMain = arr.GetLength(0);
        int lengthSec = arr.GetLength(1);

        System.Random rand = new System.Random();

        for (int i = 1; i < lengthMain; i++)
        {
            Swap(arr,lengthSec, i, i + rand.Next(lengthMain - i));
        }
    }

    private void Swap(string[,] arr, int d2Length, int a, int b)
    {
        string[] temp = new string[d2Length];
        for (int i = 0; i < d2Length; i++)
        {
            temp[i] = arr[b, i];
        }

        for (int i = 0; i < d2Length; i++)
        {
            arr[b, i] = arr[a, i];
        }

        for (int i = 0; i < d2Length; i++)
        {
            arr[a, i] = temp[i];
        }
    }
}
