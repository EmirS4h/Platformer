using UnityEngine;
using Cinemachine;
public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;
    private float shakeTimer;
    private float shakeTimerTotal;
    private float startingIntensity;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
    }
    public void Shake(float intensity, float timer)
    {
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        shakeTimer = timer;
        shakeTimerTotal = timer;
        startingIntensity = intensity;
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0.0f)
            {
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0.0f;
                Mathf.Lerp(startingIntensity, 0.0f, 1 - (shakeTimer / shakeTimerTotal));
            }
        }
    }
}
