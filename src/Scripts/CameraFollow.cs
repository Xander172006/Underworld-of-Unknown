using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform leftBoundary;
    public Transform target;         
    public float smoothSpeed = 0.125f;
    public Vector3 offset;         

   void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        float clampedX = Mathf.Max(smoothedPosition.x, leftBoundary.position.x);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
}
