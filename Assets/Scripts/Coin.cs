using UnityEngine;

public class Coin : PickUp
{
    [SerializeField] int addScoreValue = 100;
    ScoreManager scoreManager;
    void Start() => 
        scoreManager = FindFirstObjectByType<ScoreManager>();
    protected override void OnPickUp()
    {
        scoreManager.AddScore(addScoreValue);
    }
}
