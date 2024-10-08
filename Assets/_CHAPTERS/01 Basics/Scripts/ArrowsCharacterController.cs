using UnityEngine;

/// <summary>
/// Component used to control a character in the scene along X (right) and Z (forward) axes., using the arrow keys.
/// </summary>
public class ArrowsCharacterController : MonoBehaviour
{

    // Note: the [Tooltip] attribute creates a popup with a given text when you hover this field in the inspector.
    [Tooltip("Defines the speed (in units/s) of the character.")]
    public float speed;

    /// <summary>
    /// Called once per frame. We check if an arrow key is pressed every frame, so we can move the character accordingly.
    /// </summary>
    void Update()
    {
        //First, we need to store the direction to use for this frame. Coordinates in Unity are defined in 3 dimensions, and stored in a
        //class named Vector3. So here, we create a new "empty" Vector3, which is by default (0;0;0).
        Vector3 moveDirection = new Vector3();

        // The Input class contains several functions to check if a key, a mouse or even a controller button is used. In our case, we want
        // to check for each arrow key, and update the direction if it's pressed.

        // The GetKey() function takes the key to check as a parameter, and returns a boolean: true if the key is pressed, false otherwise.
        // When using conditions, you must compare a value with another. But with booleans, you can remove the comparison operator (==, !=, etc.).
        if (Input.GetKey(KeyCode.RightArrow) /* == true */)
        {
            // The right arrow key is used to move the character to the right. So we can add 1 to the X axis of the direction.
            moveDirection.x += 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // The left arrow key is used to move the character to the left. So we can substract 1 to the X axis of the direction.
            // At this step, if the player presses both right and left arrow, the X axis of the direction is reset to 0, so the character
            // won't move along the X axis.
            moveDirection.x -= 1;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            // The left arrow key is used to move the character forward. In Unity, the X axis is right, Y is up, and Z is forward. So here,
            // we can add 1 to the Z axis.
            moveDirection.z += 1;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            moveDirection.z -= 1;
        }

        // If the direction is "zero", meaning it's a (0;0;0) vector, it could mean that no arrow key is pressed this frame. To avoid
        // unnecessary operations, we can check if the vector is not "zero", and do something only in that case.
        // In condition, we can check for equality with the operator ==, but we can also check for difference with the operator !=.
        if (moveDirection != Vector3.zero)
        {
            // If the direction is a diagonal, for example up-right, we get a vector (1;0;1). In this case, the vector has a length (or
            // "magnitude") of 2. This means that if we multiply it by the speed value, the character will move twice as fast as expected.

            // This is a common problem when making a character controller. And we can solve this by normalizing the vector. This is a math
            // operation that keeps the direction of a vector, but reduce its magnitude to 1. By doing this, we can make sure that the
            // character will move in the expected position, and with the expected speed.
            moveDirection.Normalize();

            // "transform" is a shortcut to get the Transform component of the object to which this component is attached. That component is
            // used to store the position, rotation and scale of the object. So in order to move the object, we can directly set its new
            // position.

            // To calculate the new position of the object, we can add our direction vector to it, multiplied by the speed of the character.
            // There's a little trick here: we are in an Update() function, called once per frame. On a 60FPS screen, that function will be
            // called exactly 60 times per seconds. So if we just apply the speed as is, the character will move by 6 units in the scene
            // every frame, which brings two issues: it's way too fast, and will be even faster on 144FPS screens!

            // To solve this, we can multiply by Time.deltaTime. This value is the time elapsed since the previous frame. On a 60FPS screen,
            // this is 1 / 60 ~ 0.0167, reducing the speed value to apply, so the character can move at the expected speed per second.

            // You can translate a Time.deltaTime multiplication by "per second", and read the following calculation as "direction * speed
            // per second".
            transform.position += moveDirection * speed * Time.deltaTime;

            // Vector3.forward is the "forward" vector (Z axis) in World space. Transform.forward is the forward vector of an object, in
            // Local space. So by assigning the forward vector of this character, we actually rotate it in the direction of the movement.
            transform.forward = moveDirection;
        }
    }
}
