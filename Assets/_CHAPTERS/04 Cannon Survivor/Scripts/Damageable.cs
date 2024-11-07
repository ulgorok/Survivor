using UnityEngine;
using UnityEngine.Events;

// Represents an object with a health value that can be "killed".
public class Damageable : MonoBehaviour
{

    [Header("Settings")]

    [Tooltip("The amount of damages this object can take before being killed.")]
    public float maxHealth = 10f;

    [Tooltip("If enabled, this object is destroyed when it dies.")]
    public bool destroyOnDeath = false;

    [Header("Events")]

    [Tooltip("Invoked when this object dies after taking damages.")]
    public UnityEvent onDie = new UnityEvent();

    // The current health value on this object.
    private float _health = 0f;

    // Checks if this object is dead.
    public bool IsDead => _health <= 0f;

    private void Awake()
    {
        _health = maxHealth;
    }

    // Makes this object take damage, and die eventually.
    public bool TakeDamages(float damages)
    {
        // Cancel if this object is already dead
        if (IsDead)
            return false;

        _health -= damages;
        if (_health <= 0)
        {
            _health = 0;
            onDie?.Invoke();

            if (destroyOnDeath)
                Destroy(gameObject);
        }
        return true;
    }

}
