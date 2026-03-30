using UnityEngine;

public class Coin : PickUp
{
    protected override void OnPickUp()
    {
        Debug.Log("You take 100$");
    }
}
