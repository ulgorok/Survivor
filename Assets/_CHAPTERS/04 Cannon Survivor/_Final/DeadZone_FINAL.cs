using UnityEngine;

public class DeadZone_FINAL : MonoBehaviour
{

    // Called when an object exits from this trigger
    private void OnTriggerExit2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }

}
