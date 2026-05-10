using UnityEngine;

/// <summary>
/// Keeps the camera in a fixed side-view while optionally following the walker along X and Y.
/// Use orthographic mode for the clearest 2.5D platformer presentation.
/// </summary>
[RequireComponent(typeof(Camera))]
public class CameraFollow2_5D : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0f, 2f, -10f);
    [SerializeField] private bool followX = true;
    [SerializeField] private bool followY = true;
    [SerializeField] private float smoothTime = 0.15f;
    [SerializeField] private bool forceOrthographic = true;
    [SerializeField] private float orthographicSize = 5f;

    private Camera cameraComponent;
    private Vector3 velocity;

    private void Awake()
    {
        cameraComponent = GetComponent<Camera>();

        if (forceOrthographic)
        {
            cameraComponent.orthographic = true;
            cameraComponent.orthographicSize = orthographicSize;
        }
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        Vector3 desiredPosition = transform.position;

        if (followX)
        {
            desiredPosition.x = target.position.x + offset.x;
        }

        if (followY)
        {
            desiredPosition.y = target.position.y + offset.y;
        }

        desiredPosition.z = offset.z;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);

        // Side-view camera looks along the Z axis at the 2D gameplay plane.
        transform.rotation = Quaternion.identity;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
