using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _cloudParticlePrefab;
    [SerializeField] private float _minImpactForce = 10f;

    private bool _isReady = false;

    private void Start()
    {
        Invoke(nameof(SetReady), 0.5f);
    }

    private void SetReady()
    {
        _isReady = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_isReady) return;

        // Die if hit by bird
        if (collision.collider.GetComponent<Bird>() != null)
        {
            Die();
            return;
        }

        // Ignore other enemies
        if (collision.collider.GetComponent<Enemy>() != null)
        {
            return;
        }

        // Die if hit from above
        if (collision.contacts[0].normal.y < -0.5f)
        {
            Die();
            return;
        }

        // Die if falling hard onto ground
        if (collision.contacts[0].normal.y > 0.5f)
        {
            if (collision.relativeVelocity.magnitude > _minImpactForce)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        Instantiate(_cloudParticlePrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}