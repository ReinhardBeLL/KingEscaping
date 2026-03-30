using UnityEngine;

public abstract class PickUp : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 100f;
    protected GroundSpawnerScript gScript;
    const string playerString = "Player";
    void Start()
    {
        gScript = FindFirstObjectByType<GroundSpawnerScript>();
    }
    void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(playerString))
        {
            OnPickUp();
            Destroy(gameObject);
        }
    }
    protected abstract void OnPickUp();
}
