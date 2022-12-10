using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ScreenShaker : MonoBehaviour
{
    public static ScreenShaker Instance;

    public CinemachineVirtualCamera cinemachineCamera;
    public CinemachineBasicMultiChannelPerlin perlin;

    Coroutine shakeCamCoroutine;

    private void Awake()
    {
        Instance = this;
        perlin = cinemachineCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void ShakeCamera(float seconds, float amplitude, float frequency)
    {
        if (shakeCamCoroutine != null) StopCoroutine(shakeCamCoroutine);
        shakeCamCoroutine = StartCoroutine(ShakeCameraCoroutine(seconds, amplitude, frequency));
    }

    private IEnumerator ShakeCameraCoroutine(float seconds, float amplitude, float frequency)
    {
        perlin.m_AmplitudeGain = amplitude;
        perlin.m_FrequencyGain = frequency;

        yield return new WaitForSeconds(seconds);

        perlin.m_AmplitudeGain = 0;
        perlin.m_FrequencyGain = 0;

        shakeCamCoroutine = null;
    }
}