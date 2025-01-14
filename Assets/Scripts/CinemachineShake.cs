using Cinemachine;
using UnityEngine;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance { get; private set; }
    
    [SerializeField] private CinemachineFreeLook _cinemachineCamera;

    private float _shakeTime;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if(_shakeTime > 0.0f)
        {
            _shakeTime -= Time.deltaTime;
            if (_shakeTime <= 0.0f)
            {
                _cinemachineCamera.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
                _cinemachineCamera.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
                _cinemachineCamera.GetRig(2).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
            }
        }
    }


    public void ShakeCamera(float intensity, float duration)
    {
        _cinemachineCamera.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = intensity;
        _cinemachineCamera.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = intensity;
        _cinemachineCamera.GetRig(2).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = intensity;

        _cinemachineCamera.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = duration;
        _cinemachineCamera.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = duration;
        _cinemachineCamera.GetRig(2).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = duration;
        _shakeTime = duration;
    }
}
