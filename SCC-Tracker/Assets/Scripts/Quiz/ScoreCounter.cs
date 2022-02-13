using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public float currentScore;
    public int maxQuestionScore;
    public float scoreComboMultiplier = 1;
    public int currentCombo;
    public float questionTime;
    public float maxScore;

    private float startTime;
    private float currentBaseScore;

    public void Update()
    {
        float t = (Time.time - startTime) / questionTime;
        t = Mathf.Sin(t * Mathf.PI * 0.5f);
        currentBaseScore = Mathf.SmoothStep(maxQuestionScore, 0, t);
    }

    private void OnEnable()
    {
        maxScore = 0;
        for (int i = 0; i < 5; i++)
        {
            maxScore += maxQuestionScore * (scoreComboMultiplier + i / 10);
        }

        StartTime();
    }

    public void StartTime()
    {
        startTime = Time.time;
    }

    public void CorrectAns()
    {
        currentScore += currentBaseScore * (scoreComboMultiplier + currentCombo / 10);
        currentCombo++;
        StartTime();
    }

    public void WrongAns()
    {
        currentCombo = 0;
        StartTime();
    }

    public void ClearScore()
    {
        currentScore = 0;
        currentCombo = 0;
    }
}
