using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;
    [SerializeField] TMP_Text gameOverText;
    [SerializeField] KingControl kingControl;
    bool isGameOver = false;
    float startTimerValue = 0f;
    float timeDuration = 5f;
    void Start()
    {
        startTimerValue = timeDuration;
        gameOverText.enabled = false;
    }
    void Update()
    {
        if(isGameOver) return;

        startTimerValue -= Time.deltaTime;
        timerText.text = startTimerValue.ToString("F1");
        if(startTimerValue < 0f)
        {
            GameOver();
        }
    }
    void GameOver()
    {
        Time.timeScale = .1f;
        kingControl.enabled = false;
        gameOverText.enabled = true;
        isGameOver = true;
        enabled = false;
    }
}
