using UnityEngine;

public class KingControl : MonoBehaviour
{
    [SerializeField] float speedMovement = 10f;
    const float clampingX = 4f;
    Playercontrolaction control;
    Rigidbody rb;
    Vector2 inputMove;
    void OnEnable() => 
        control.Enable();
    void OnDisable() => 
        control.Disable();
    void Awake()
    {
        control = new Playercontrolaction();
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        PlayerMovement();
    }
    void PlayerMovement()
    {
        Vector3 pos = rb.position;
        inputMove = control.Movement.Move.ReadValue<Vector2>();
        Vector3 dir = new Vector3(inputMove.x, 0f, inputMove.y) * speedMovement * Time.fixedDeltaTime;

        pos += dir;
        pos.x = Mathf.Clamp(pos.x, -clampingX, clampingX);
        pos.z = Mathf.Clamp(pos.z, -2, 1.5f);

        rb.MovePosition(pos);        
    }
}
