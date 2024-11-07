using UnityEngine;

public class Projectile : MonoBehaviour
{

    [Header("Settings")]

    [Tooltip("The distance to which this projectile can detect a collision with an enemy.")]
    public float radius = 1f;

    [Tooltip("The collision layer to use to detect enemy.")]
    public LayerMask targetLayer = ~0;

    // The damages to apply to an enemy hit by this projectile.
    private float _damages = 0f;

    // The speed (in units/s) at which this projectile moves in the scene.
    private float _speed = 0f;

    // The distance (in units) this projectile can travel before expiring.
    private float _range = 0f;

    // The position of this projectile when created in the scene.
    private Vector3 _origin = Vector3.zero;

    private void Start()
    {
        _origin = transform.position;
    }

    private void Update()
    {
        // Project the movement of this projectile for the current frame
        float distance = _speed * Time.deltaTime;
        RaycastHit2D hitInfo = Physics2D.CircleCast(transform.position, radius, transform.up, distance, targetLayer);
        // If the projectile is about to hit an enemy (the raycast did hit sometthing with a Damageable component attached)
        if (hitInfo.collider != null && hitInfo.collider.TryGetComponent(out Damageable damageable))
        {
            // Make the target take damage
            damageable.TakeDamages(_damages);
            // Destroy this projectile instance
            Destroy(gameObject);
            return;
        }
        // Else, if the projectile won't hit anything this frame, just make it move
        else
        {
            transform.position += transform.up * _speed * Time.deltaTime;
        }

        // If the distance travelled by this projectile is out of range
        if (Vector3.Distance(transform.position, _origin) >= _range)
        {
            // Destroy this projectile instance
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Launches this projectile, making it move in the scene.
    /// </summary>
    /// <param name="damages">The amount of damages</param>
    /// <param name="direction">The direction to which this projectile moves.</param>
    /// <param name="range">The maximum distance (in iunits) that can be travelled by this projectile before it expires.</param>
    public void Launch(float damages, float speed, Vector2 direction, float range)
    {
        // Set projectile values
        _damages = damages;
        _range = range;
        _speed = speed;
        // Make this projectile rotate in the given direction
        transform.up = direction;
    }

    // Draw custom gizmos in the scene only when this object is selected.
    private void OnDrawGizmosSelected()
    {
        // Set the color of the next gizmos to draw
        Gizmos.color = Color.yellow;
        // Draw a gizmo to represent this projectile's detection radius.
        Gizmos.DrawWireSphere(transform.position, radius);
        // Draw a line that represents the direction of this projectile
        Gizmos.DrawLine(transform.position, transform.position + transform.up * _speed);
    }

}
