using UnityEngine;
using UnityEngine.VFX;

public class ExplosionEffect : MonoBehaviour
{
    public float cameraShakeIntensity = 2.0f;
    public float cameraShakeDuration = 1.0f;

    [SerializeField] private VisualEffect _sparkParticles;

    void Awake()
    {
        _sparkParticles.Stop();
    }

    public void ShowExplosion()
    {
        _sparkParticles.Play();
        CinemachineShake.Instance.ShakeCamera(cameraShakeIntensity, cameraShakeDuration);
    }

    public void OnDespawn()
    {
        _sparkParticles.Stop();
        Destroy(this.gameObject);
    }
}
