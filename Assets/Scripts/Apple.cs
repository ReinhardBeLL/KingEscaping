using UnityEngine;

public class Apple : PickUp
{
    float boostSpeedGround = 3f;
    protected override void OnPickUp()
    {
        gScript.ChangeSpeedOnCollision(boostSpeedGround);
    }
}
