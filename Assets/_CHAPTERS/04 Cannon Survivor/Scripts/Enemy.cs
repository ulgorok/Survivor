using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{

    [Tooltip("The speed (in units/s) at which this enemy moves in the scene.")]
    public float speed = 1f;

    private Rigidbody2D _rigidbody = null;

    private void Awake()
    {
        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Move this enemy (using its rigidbody, so collisions are processed as expected by the physics engine).
        _rigidbody.position += (Vector2)transform.up * speed * Time.deltaTime;
    }

}
