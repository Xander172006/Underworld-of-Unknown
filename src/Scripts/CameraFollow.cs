using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // --- set a boundary and target for the camera to follow
    public Transform leftBoundary;
    public Transform target;         
    public float smoothSpeed = 0.125f;
    public Vector3 offset;         

   void LateUpdate()
    {
        if (target == null) return;

        // -- Calculate desired position based on the target's position
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // -- Clamp the camera's position to the left boundary as to not go past it
        float clampedX = Mathf.Max(smoothedPosition.x, leftBoundary.position.x);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
}
