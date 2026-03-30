using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraSettings : MonoBehaviour
{
    [SerializeField] ParticleSystem speedUpParticle;
    [SerializeField] float minFOV = 30f;
    [SerializeField] float maxFOV = 90f;
    [SerializeField] float cameraZoomChangeSpeed = 15f;
    [SerializeField] CinemachineCamera cineCamera;
    Coroutine currentCorutine;
    public void CameraZoomChangeFOV(float speed)
    {
        
        if(currentCorutine != null)
        {
            StopCoroutine(currentCorutine);
        }
        currentCorutine = StartCoroutine(CameraFOVdynamicChange(speed));
    }
    void Update()
    {
        SpeedUpEffect();
    }
    void SpeedUpEffect()
    {
        var emission = speedUpParticle.emission;
        float FOV = cineCamera.Lens.FieldOfView;

        if(FOV > 80f && !emission.enabled)
        {
            emission.enabled = true;
        }
        else if(FOV < 80f && emission.enabled)
        {
            emission.enabled = false;
        }
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
            yield return null;
        }
        cineCamera.Lens.FieldOfView = targetFOV;
        currentCorutine = null;
    }
}
