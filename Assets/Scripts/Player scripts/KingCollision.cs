using UnityEngine;

public class KingCollision : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GroundSpawnerScript gScript;
    float ajustSpeedChange = -2f;
    float collisionCoolDown = 1f;
    float coolDownTime = 0f;
    void Update()
    {
        coolDownTime += Time.time;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (coolDownTime < collisionCoolDown) return;
        gScript.ChangeSpeedOnCollision(ajustSpeedChange);
        animator.SetTrigger("Trigger hit");
        coolDownTime = 0;
    }
}
