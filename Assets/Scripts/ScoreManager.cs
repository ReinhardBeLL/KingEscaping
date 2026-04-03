using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreBoard;
    int startScore = 0;
    void Start()
    {
        if(scoreBoard == null)
        {
            Debug.LogWarning("ScoreManager: ScoreBoard is not assigned");
            enabled = false;
        }
    }
    public void AddScore(int score)
    {
        startScore += score;
        scoreBoard.text = startScore.ToString();
    }
}
