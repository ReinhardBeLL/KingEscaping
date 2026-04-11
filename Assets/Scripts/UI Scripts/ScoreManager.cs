using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreBoard;
    GameManager gameManager;
    int currentScore = 0;
    void Start()
    {
        gameManager = GetComponent<GameManager>();
        if(scoreBoard == null)
        {
            Debug.LogWarning("ScoreManager: ScoreBoard is not assigned");
            enabled = false;
        }
    }
    void UpdateScoreUI()
    {
        if(gameManager.GameOver) return;
        scoreBoard.text = currentScore.ToString();
    }
    public void AddScore(int score)
    {
        currentScore += score;
        UpdateScoreUI();
    }
}
