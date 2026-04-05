using UnityEngine;

public abstract class PickUp : MonoBehaviour
{
    [Header("Torque multiplier")]
    [SerializeField] float rotationSpeed = 100f;
    protected GroundSpawnerScript gScript;
    public void Init(GroundSpawnerScript gScript) =>
        this.gScript = gScript;
    void Update() =>
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            OnPickUp();
            Destroy(gameObject);
        }
    }
    protected abstract void OnPickUp();
}
