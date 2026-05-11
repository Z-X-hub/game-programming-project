using UnityEngine;

/// <summary>
/// Moves a 3D platform between two points while keeping the gameplay 2.5D.
/// It can be activated by selection input or by a switch.
/// </summary>
[RequireComponent(typeof(Collider))]
public class MovingPlatform3D : MonoBehaviour, IWorldActivatable
{
    [Header("Path")]
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private Vector3 endOffset = new Vector3(3f, 0f, 0f);
    [SerializeField] private float moveSpeed = 2f;

    [Header("Activation")]
    [SerializeField] private bool startActive;
    [SerializeField] private bool toggleTargetOnActivate = true;
    [SerializeField] private bool pingPongWhileActive;
    [SerializeField] private bool returnToStartWhenInactive = true;

    [Header("2.5D Platform Feel")]
    [SerializeField] private bool carryWalkerOnTop = true;

    private Rigidbody rb;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private Vector3 targetPosition;
    private bool isActive;
    private bool movingToEnd = true;
    private AutoWalker3D carriedWalker;
    private Transform carriedWalkerOriginalParent;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.isKinematic = true;
        }

        startPosition = startPoint != null ? startPoint.position : transform.position;
        endPosition = endPoint != null ? endPoint.position : startPosition + endOffset;
        isActive = startActive;
        movingToEnd = startActive;
        targetPosition = movingToEnd ? endPosition : startPosition;
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance != null && !GameManager.Instance.IsGameplayActive)
        {
            return;
        }

        UpdateTarget();
        MoveTowardsTarget();
    }

    private void OnDisable()
    {
        ReleaseWalker();
    }

    public void Activate()
    {
        if (toggleTargetOnActivate)
        {
            movingToEnd = !movingToEnd;
            isActive = true;
            return;
        }

        isActive = true;
        movingToEnd = true;
    }

    public void Deactivate()
    {
        isActive = false;

        if (returnToStartWhenInactive)
        {
            movingToEnd = false;
        }
    }

    private void UpdateTarget()
    {
        if (!isActive && !returnToStartWhenInactive)
        {
            targetPosition = transform.position;
            return;
        }

        targetPosition = movingToEnd ? endPosition : startPosition;
    }

    private void MoveTowardsTarget()
    {
        Vector3 currentPosition = rb != null ? rb.position : transform.position;
        Vector3 nextPosition = Vector3.MoveTowards(currentPosition, targetPosition, moveSpeed * Time.fixedDeltaTime);

        if (rb != null)
        {
            rb.MovePosition(nextPosition);
        }
        else
        {
            transform.position = nextPosition;
        }

        if (Vector3.Distance(nextPosition, targetPosition) <= 0.01f && pingPongWhileActive && isActive)
        {
            movingToEnd = !movingToEnd;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        TryCarryWalker(collision);
    }

    private void OnCollisionStay(Collision collision)
    {
        TryCarryWalker(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        AutoWalker3D walker = collision.collider.GetComponentInParent<AutoWalker3D>();
        if (walker != null && walker == carriedWalker)
        {
            ReleaseWalker();
        }
    }

    private void TryCarryWalker(Collision collision)
    {
        if (!carryWalkerOnTop || carriedWalker != null)
        {
            return;
        }

        AutoWalker3D walker = collision.collider.GetComponentInParent<AutoWalker3D>();
        if (walker == null || !CollisionIsOnTop(collision, walker.transform))
        {
            return;
        }

        carriedWalker = walker;
        carriedWalkerOriginalParent = walker.transform.parent;
        walker.transform.SetParent(transform, true);
    }

    private bool CollisionIsOnTop(Collision collision, Transform walkerTransform)
    {
        if (walkerTransform.position.y <= transform.position.y)
        {
            return false;
        }

        for (int i = 0; i < collision.contactCount; i++)
        {
            ContactPoint contact = collision.GetContact(i);

            // A contact normal with strong Y means the walker is standing on the platform face.
            if (Mathf.Abs(contact.normal.y) > 0.5f)
            {
                return true;
            }
        }

        return false;
    }

    private void ReleaseWalker()
    {
        if (carriedWalker == null)
        {
            return;
        }

        carriedWalker.transform.SetParent(carriedWalkerOriginalParent, true);
        carriedWalker = null;
        carriedWalkerOriginalParent = null;
    }
}
