using UnityEngine;

public class ChunckCheckPoint : MonoBehaviour
{
    GameManager gameManager;
    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            gameManager.AddTimerNumber(5f);
        }
    }
}
