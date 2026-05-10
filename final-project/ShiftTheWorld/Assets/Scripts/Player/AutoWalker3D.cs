using UnityEngine;

/// <summary>
/// Automatically moves the player character along the X axis.
/// The player never controls this script directly; they guide the walker by changing the level.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class AutoWalker3D : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float walkSpeed = 2.2f;
    [SerializeField] private int startDirection = 1;
    [SerializeField] private bool turnAroundWhenBlocked = true;
    [SerializeField] private bool startWalkingOnPlay = true;

    [Header("2.5D Constraint")]
    [SerializeField] private bool freezeDepthPosition = true;

    private Rigidbody rb;
    private int currentDirection;
    private bool isWalking;
    private Vector3 startPosition;
    private Quaternion startRotation;
    private float lockedZ;

    public bool IsWalking
    {
        get { return isWalking; }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
        startRotation = transform.rotation;
        lockedZ = transform.position.z;
        currentDirection = startDirection >= 0 ? 1 : -1;

        rb.constraints |= RigidbodyConstraints.FreezeRotation;

        if (freezeDepthPosition)
        {
            rb.constraints |= RigidbodyConstraints.FreezePositionZ;
        }
    }

    private void Start()
    {
        isWalking = startWalkingOnPlay;
    }

    private void FixedUpdate()
    {
        if (!isWalking || IsLevelStopped())
        {
            KeepDepthLocked();
            return;
        }

        Vector3 velocity = rb.velocity;
        velocity.x = currentDirection * walkSpeed;
        velocity.z = 0f;
        rb.velocity = velocity;

        KeepDepthLocked();
        FaceWalkDirection();
    }

    private void OnCollisionEnter(Collision collision)
    {
        CheckForWallCollision(collision);
    }

    private void OnCollisionStay(Collision collision)
    {
        CheckForWallCollision(collision);
    }

    public void StartWalking()
    {
        isWalking = true;
    }

    public void StopWalking()
    {
        isWalking = false;
        rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
    }

    public void ResetWalker()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        currentDirection = startDirection >= 0 ? 1 : -1;
        isWalking = startWalkingOnPlay;
    }

    private bool IsLevelStopped()
    {
        return GameManager.Instance != null && !GameManager.Instance.IsGameplayActive;
    }

    private void CheckForWallCollision(Collision collision)
    {
        if (!isWalking || IsLevelStopped())
        {
            return;
        }

        for (int i = 0; i < collision.contactCount; i++)
        {
            ContactPoint contact = collision.GetContact(i);

            // Side normals indicate a wall or blocking object. Upward normals are normal ground contact.
            if (Mathf.Abs(contact.normal.x) < 0.55f)
            {
                continue;
            }

            bool hitObjectInFront = Mathf.Sign(contact.normal.x) != currentDirection;
            if (!hitObjectInFront)
            {
                continue;
            }

            if (turnAroundWhenBlocked)
            {
                currentDirection *= -1;
            }
            else
            {
                StopWalking();
            }

            return;
        }
    }

    private void FaceWalkDirection()
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * currentDirection;
        transform.localScale = scale;
    }

    private void KeepDepthLocked()
    {
        if (!freezeDepthPosition)
        {
            return;
        }

        Vector3 position = transform.position;
        position.z = lockedZ;
        transform.position = position;
    }
}
