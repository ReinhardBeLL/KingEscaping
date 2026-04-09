using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreBoard;
    int currentScore = 0;
    void Start()
    {
        if(scoreBoard == null)
        {
            Debug.LogWarning("ScoreManager: ScoreBoard is not assigned");
            enabled = false;
        }
    }
    void UpdateScoreUI()
    {
        scoreBoard.text = currentScore.ToString();
    }
    public void AddScore(int score)
    {
        currentScore += score;
        UpdateScoreUI();
    }
}
