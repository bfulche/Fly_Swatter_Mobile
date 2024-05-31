using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text roundScoreText;

    private int roundScore;

    private void Start()
    {
        roundScore = 0;
        UpdateRoundScoreText();
    }

    public void IncrementRoundScore()
    {
        roundScore++;
        UpdateRoundScoreText();
    }

    private void UpdateRoundScoreText()
    {
        roundScoreText.text = "Round Score: " + roundScore.ToString();
    }
}
