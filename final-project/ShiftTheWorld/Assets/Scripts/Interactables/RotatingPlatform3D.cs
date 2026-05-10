using UnityEngine;

/// <summary>
/// Smoothly rotates a platform or block in 90 degree steps.
/// The default axis is Z, which works well for side-view 2.5D platform puzzles.
/// </summary>
public class RotatingPlatform3D : MonoBehaviour, IWorldActivatable, IWorldRotatable
{
    [Header("Rotation")]
    [SerializeField] private Vector3 localRotationAxis = Vector3.forward;
    [SerializeField] private float stepAngle = 90f;
    [SerializeField] private float rotationSpeed = 180f;
    [SerializeField] private int defaultActivateDirection = 1;

    private Quaternion targetRotation;
    private bool isRotating;

    private void Awake()
    {
        targetRotation = transform.localRotation;
    }

    private void Update()
    {
        if (!isRotating)
        {
            return;
        }

        transform.localRotation = Quaternion.RotateTowards(
            transform.localRotation,
            targetRotation,
            rotationSpeed * Time.deltaTime);

        if (Quaternion.Angle(transform.localRotation, targetRotation) <= 0.1f)
        {
            transform.localRotation = targetRotation;
            isRotating = false;
        }
    }

    public void Activate()
    {
        RotateByStep(defaultActivateDirection);
    }

    public void Deactivate()
    {
        // Rotating blocks do not need a separate deactivation in this prototype.
    }

    public void RotateByStep(int direction)
    {
        if (direction == 0 || isRotating)
        {
            return;
        }

        Vector3 axis = localRotationAxis.sqrMagnitude > 0f ? localRotationAxis.normalized : Vector3.forward;
        targetRotation = targetRotation * Quaternion.AngleAxis(stepAngle * Mathf.Sign(direction), axis);
        isRotating = true;
    }
}
