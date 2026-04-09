using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;
    [SerializeField] TMP_Text gameOverText;
    [SerializeField] KingControl kingControl;
    [SerializeField] float slowMotionValue = .1f;
    [SerializeField] float timeDuration = 5f;
    bool isGameOver = false;
    float currentTime = 0f;
    void Start()
    {
        if(timerText == null || gameOverText == null || kingControl == null)
        {
            Debug.LogWarning("GameManager: Assign all references in inspector");
            enabled = false;
        }
        currentTime = timeDuration;
        gameOverText.enabled = false;
        Time.timeScale = 1f;
    }
    void Update() => TimerCountDown();
    void TimerCountDown()
    {
        if(isGameOver) return;

        currentTime -= Time.deltaTime;
        currentTime = Mathf.Clamp(currentTime, 0f, timeDuration);
        UpdateTextUI();
        if(currentTime <= 0f)
            GameOver();
    }
    void UpdateTextUI()
    {
        timerText.text = Mathf.Max(currentTime, 0f).ToString();
    }
    void GameOver()
    {
        if(isGameOver) return;
        Time.timeScale = slowMotionValue;
        kingControl.enabled = false;
        gameOverText.enabled = true;
        isGameOver = true;
    }
}
