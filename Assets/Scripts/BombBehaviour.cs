using UnityEngine;

public class BombBehaviour : MonoBehaviour
{
    public float explosionDuration = 0.5f;
    public float explosionForce = 5.0f;
    public float upwardsExplosionModifier = 1f;
    public float explosionRadius = 7.5f;

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private GameObject _explosionEffect;

    public void Explode()
    {
        // Freeze and reset velocity of the rigidbody to prevent it of moving while exploding
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.isKinematic = true;
        _rigidbody.freezeRotation = true;

        // Add explosion force
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, 0.0f ,ForceMode.Impulse);
            }
        }

        Instantiate(_explosionEffect, transform.position, transform.rotation, null);
        Destroy(gameObject);
    }
}
