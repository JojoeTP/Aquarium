using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerCam : MonoBehaviour
{
    // Start is called before the first frame update
    public CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] float amplitude;
    [SerializeField] float frequency;
    [SerializeField] float shakeCameraTime;
    CinemachineBasicMultiChannelPerlin cameraInfo;
    void Start()
    {
        if(cinemachineVirtualCamera != null)
            cameraInfo = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    IEnumerator ShakeCameraTime(float time)
    {
        yield return new WaitForSeconds(1f);
        ShakeStateCamera();
        yield return new WaitForSeconds(time);
        NormalStateCamera();
    }

    public void ShakeCamera()
    {
        StopAllCoroutines();
        StartCoroutine(ShakeCameraTime(shakeCameraTime));
    }

    void ShakeStateCamera()
    {
        cameraInfo.m_AmplitudeGain = amplitude;
        cameraInfo.m_FrequencyGain = frequency;
    }
    void NormalStateCamera()
    {
        cameraInfo.m_AmplitudeGain = 0;
        cameraInfo.m_FrequencyGain = 0;
    }



}
