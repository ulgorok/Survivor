using UnityEngine;

// Spawns enemy instances at runtime.
public class EnemiesSpawner_FINAL : MonoBehaviour
{

    [Header("References")]

    [Tooltip("The object that represents the player that spawned enemies should focus.")]
    public Transform targetPlayer = null;

    [Tooltip("The prefabs of the enemies that can be spawned from this spawner.")]
    public Enemy[] enemyPrefabs = { };

    [Header("Settings")]

    [Tooltip("The distance (in units) at which enemies are spawned.")]
    public float distance = 10f;

    [Min(0.01f)]
    [Tooltip("The minimum time (in seconds) to wait before spawning an enemy again.")]
    public float minSpawnInterval = .2f;

    [Min(0.01f)]
    [Tooltip("The maximum time (in seconds) to wait before spawning an enemy again.")]
    public float maxSpawnInterval = 1f;

    [Min(0.01f)]
    [Tooltip("The time (in seconds) for this spawner to reach the minimum spawn interval.")]
    public float spawnIntervalReductionDuration = 60f;

    // The time (in seconds) to wait before spawning an enemy.
    private float _spawnCooldown = 0f;

    private void Update()
    {
        // Update the spawn cooldown if it's running
        if (_spawnCooldown > 0f)
        {
            _spawnCooldown -= Time.deltaTime;
        }

        // If the spawn cooldown has reached its minimum value
        if (_spawnCooldown <= 0)
        {
            SpawnEnemy();

            // Get the ratio of the spawn interval reduction: 0 means the level has just loaded, 1 means the the level was loaded for a
            // time greater or equal to the spawn interval reduction duration.
            float timeRatio = Time.timeSinceLevelLoad > 0 ? Mathf.Clamp01(spawnIntervalReductionDuration / Time.timeSinceLevelLoad) : 0;
            // Reset the spawn cooldown, using an interpolation between the maximum and minimum spawn interval value, using the ratio as
            // the "time" parameter. Depending on that ratio:
            // - If the ratio is 0, the maxSpawnInterval value is used
            // - If the ratio is 1, the minSpawnInterval is used
            // - If the ratio is 0.5, the exact middle point between min and max spawn interval is used
            _spawnCooldown = Mathf.Lerp(maxSpawnInterval, minSpawnInterval, timeRatio);
        }
    }

    // Makes an enemy spawn.
    private void SpawnEnemy()
    {
        // Select an enemy prefab at random in the list
        Enemy enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        // Calculate a random direction and normalize it
        Vector3 spawnDirection = Random.insideUnitCircle;
        spawnDirection.Normalize();
        // Calculate the position of the enemy to spawn (this spawner position + the random direction * the spawn distance).
        Vector3 spawnPosition = transform.position + spawnDirection * distance;

        // Instantiate the selected enemy prefab
        Enemy enemyInstance = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        // Make the enemy head toward the player
        enemyInstance.transform.up = targetPlayer.position - enemyInstance.transform.position;
    }

    private void OnDrawGizmos()
    {
        // Set the color of the next gizmos to draw
        Gizmos.color = Color.red;
        // Draw a gizmo that represents the distance at which enemies are spawned from the player.
        Gizmos.DrawWireSphere(transform.position, distance);
    }


}
