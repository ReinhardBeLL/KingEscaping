using UnityEngine;

public class JunkClearing : MonoBehaviour
{
    void OnTriggerEnter(Collider other) =>
        Destroy(other.gameObject);
}
