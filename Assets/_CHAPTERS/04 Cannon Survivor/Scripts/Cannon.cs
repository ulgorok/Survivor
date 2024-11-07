using UnityEngine;

[RequireComponent(typeof(Damageable))]
public class Cannon : MonoBehaviour
{

    [Header("References")]

    [Tooltip("The object to rotate to feedback the shoot direction.")]
    public Transform cannonRenderer = null;

    [Tooltip("The origin position of the projectiles to shoot.")]
    public Transform cannonProjectileOrigin = null;

    [Header("Initial Settings")]

    [Tooltip("The weapon currently used by the cannon.")]
    public WeaponAsset weapon = null;

    [Tooltip("The multiplier to apply to the fire rate of the current weapon.")]
    public float fireRateMultiplier = 3f;

    [Tooltip("The multiplier to apply to projectiles' damages.")]
    public float damagesMultiplier = 1f;

    [Tooltip("The distance (in units) to which the cannon can shoot projectiles.")]
    public float range = 6f;

    private Damageable _damageable = null;
    private float _fireCooldown = 0f;

    private void Awake()
    {
        if (_damageable == null)
            _damageable = GetComponent<Damageable>();
    }

    void Update()
    {
        // Converts the mouse cursor position into a World position
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = transform.position.z;
        // Make the cannon rotate toward the cursor position
        cannonRenderer.transform.up = worldPosition - transform.position;

        // Update the fire cooldown if it's running
        if (_fireCooldown > 0f)
            _fireCooldown -= Time.deltaTime;

        // If the left mouse button is hold down, and the fire cooldown is not running
        if (Input.GetMouseButton(0) && _fireCooldown <= 0f)
        {
            // Make the cannon shoot
            Fire();
            // Reset the fire timer
            _fireCooldown = weapon.fireRate / fireRateMultiplier;
        }
    }

    /// <summary>
    /// Makes this cannon emit a projectile.
    /// </summary>
    private void Fire()
    {
        Projectile projectileInstantiate = Instantiate(weapon.projectilePrefab, cannonProjectileOrigin.position, Quaternion.identity);
        projectileInstantiate.Launch(weapon.damages, weapon.speed, cannonRenderer.transform.up, range);
        /**
         *  @todo
         *  - Instantiate the projectile prefab of the active weapon
         *  - Call Projectile.Launch() to initiate the projectile's movement
         */
    }

    // Called when an object collides with this one
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            Destroy(enemy.gameObject);
            _damageable.TakeDamages(1);
        }
    }

}
