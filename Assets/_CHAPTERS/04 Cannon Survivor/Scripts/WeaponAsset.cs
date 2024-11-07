using UnityEngine;

[CreateAssetMenu(menuName = "Cannon Survivor/Weapon", fileName = "NewWeapon")]
public class WeaponAsset : ScriptableObject
{

    [Tooltip("The time (in seconds) to wait by default before the cannon can shoot again with this weapon.")]
    public float fireRate = 1f;

    [Tooltip("The amount of damage dealt to enemies by default when a projectile from this weapon hit one of them.")]
    public float damages = 1f;

    [Tooltip("The speedd (in units/s) at which a projectile shot from this weapon moves in the scene.")]
    public float speed = 1f;

    [Tooltip("The prefab that represents a projectile shot from this weapon.")]
    public Projectile projectilePrefab = null;

}
