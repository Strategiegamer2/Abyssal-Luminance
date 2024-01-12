using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Player's transform
    public float offsetY = 2f; // Vertical offset from the player
    public float smoothTime = 0.3f; // Time taken for the camera to catch up to the player
    public float offsetChangeSpeed = 1f; // Speed at which the offset changes

    private Vector3 velocity = Vector3.zero;
    private float currentOffsetX = 3f; // Current horizontal offset from the player
    private float targetOffsetX = 3f; // Target horizontal offset from the player

    void LateUpdate()
    {
        // Determine the direction the player is moving (right or left)
        float playerDirection = player.GetComponent<Rigidbody2D>().velocity.x;

        // Update the target offset based on the player's direction
        if (playerDirection > 0) // Moving right
        {
            targetOffsetX = 3f;
        }
        else if (playerDirection < 0) // Moving left
        {
            targetOffsetX = -3f;
        }

        // Smoothly update the current offset to the target offset
        currentOffsetX = Mathf.Lerp(currentOffsetX, targetOffsetX, offsetChangeSpeed * Time.deltaTime);

        // Calculate target position with updated offset
        Vector3 targetPosition = new Vector3(
            player.position.x + currentOffsetX,
            player.position.y + offsetY,
            transform.position.z);

        // Smoothly move the camera towards the target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}