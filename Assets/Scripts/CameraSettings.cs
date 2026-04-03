using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraSettings : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] CinemachineCamera cineCamera;
    [Tooltip("Controls how fast the FOV changes")]
    [SerializeField] float cameraZoomChangeSpeed = 15f;
    [SerializeField] float fovEffectThreshold = 80f;
    [SerializeField] float minFOV = 30f;
    [SerializeField] float maxFOV = 90f;

    [Header("Speed Boost Effect")]
    [Tooltip("When FOV exceeds threshold")]
    [SerializeField] ParticleSystem speedUpParticle;

    Coroutine currentCoroutine;
    void Start()
    {
        if(speedUpParticle == null || cineCamera == null)
        {
            Debug.LogWarning("CameraSettings: SpeedUpParticle or CinemachineCamera is not assigned");
            enabled = false;
        }
    }
    public void CameraZoomChangeFOV(float speed)
    {
        if(currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(CameraFOVdynamicChange(speed));
    }
    void SpeedUpEffect()
    {
        var emission = speedUpParticle.emission;
        float FOV = cineCamera.Lens.FieldOfView;

        if(FOV >= fovEffectThreshold && !emission.enabled)
            emission.enabled = true;
        else if(FOV < fovEffectThreshold && emission.enabled)
            emission.enabled = false;
    }
    IEnumerator CameraFOVdynamicChange(float speed)
    {
        float currentFOV = cineCamera.Lens.FieldOfView;
        float targetFOV = Mathf.Clamp(currentFOV + speed * cameraZoomChangeSpeed, minFOV, maxFOV);
        float FOVDuration = 0f;
        while(FOVDuration < 1f)
        {
            FOVDuration += Time.deltaTime;
            float t = FOVDuration / 1f;
            cineCamera.Lens.FieldOfView = Mathf.Lerp(currentFOV, targetFOV, t);
            SpeedUpEffect();
            yield return null;
        }
        cineCamera.Lens.FieldOfView = targetFOV;
        currentCoroutine = null;
    }
}
