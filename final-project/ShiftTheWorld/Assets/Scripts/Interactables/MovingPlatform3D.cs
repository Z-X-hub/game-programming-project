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

    private Rigidbody rb;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private Vector3 targetPosition;
    private bool isActive;
    private bool movingToEnd = true;

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
}
