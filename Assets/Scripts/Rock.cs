using Unity.Cinemachine;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] ParticleSystem collisionParticleSystem;
    [SerializeField] AudioSource audioSource;
    CinemachineImpulseSource cinemachineImpulseSource;
    void Start() => cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    void OnCollisionEnter(Collision collision)
    {
        CollisionMethdot();
        CollisionFX(collision);
    }
    void CollisionMethdot()
    {
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        float shaking = Mathf.Clamp(1f / distance, 0f, 1f);
        cinemachineImpulseSource.GenerateImpulse(shaking);
    }
    void CollisionFX(Collision other)
    {
        ContactPoint contactPoint = other.contacts[0];
        collisionParticleSystem.transform.position = contactPoint.point;
        collisionParticleSystem.Play();
        if(!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
