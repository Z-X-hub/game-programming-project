using UnityEngine;

/// <summary>
/// Opens and closes a simple 3D door or gate when activated by a switch.
/// </summary>
[RequireComponent(typeof(Collider))]
public class DoorController3D : MonoBehaviour, IWorldActivatable
{
    [Header("Door Movement")]
    [SerializeField] private Vector3 openOffset = new Vector3(0f, 2.5f, 0f);
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private bool startOpen;
    [SerializeField] private bool disableColliderWhenOpen = true;

    [Header("Feedback")]
    [SerializeField] private Renderer[] feedbackRenderers;
    [SerializeField] private Color closedColor = new Color(0.95f, 0.35f, 0.25f, 1f);
    [SerializeField] private Color openColor = new Color(0.35f, 0.9f, 1f, 1f);

    private Rigidbody rb;
    private Collider doorCollider;
    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isOpen;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        doorCollider = GetComponent<Collider>();
        closedPosition = transform.position;
        openPosition = closedPosition + openOffset;

        if (rb != null)
        {
            rb.isKinematic = true;
        }

        isOpen = startOpen;
        SetDoorPositionInstant(isOpen ? openPosition : closedPosition);
        UpdateFeedback();
    }

    private void Update()
    {
        Vector3 target = isOpen ? openPosition : closedPosition;
        Vector3 nextPosition = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

        if (rb != null)
        {
            rb.MovePosition(nextPosition);
        }
        else
        {
            transform.position = nextPosition;
        }

        UpdateCollider();
    }

    public void Activate()
    {
        isOpen = true;
        UpdateFeedback();
    }

    public void Deactivate()
    {
        isOpen = false;
        UpdateFeedback();
    }

    private void SetDoorPositionInstant(Vector3 position)
    {
        if (rb != null)
        {
            rb.position = position;
        }

        transform.position = position;
    }

    private void UpdateCollider()
    {
        if (!disableColliderWhenOpen || doorCollider == null)
        {
            return;
        }

        bool reachedOpenPosition = Vector3.Distance(transform.position, openPosition) <= 0.05f;
        doorCollider.enabled = !isOpen || !reachedOpenPosition;
    }

    private void UpdateFeedback()
    {
        if (feedbackRenderers == null || feedbackRenderers.Length == 0)
        {
            feedbackRenderers = GetComponentsInChildren<Renderer>();
        }

        Color color = isOpen ? openColor : closedColor;

        for (int i = 0; i < feedbackRenderers.Length; i++)
        {
            Renderer targetRenderer = feedbackRenderers[i];
            if (targetRenderer == null)
            {
                continue;
            }

            Material material = targetRenderer.material;
            if (material.HasProperty("_BaseColor"))
            {
                material.SetColor("_BaseColor", color);
            }
            else if (material.HasProperty("_Color"))
            {
                material.SetColor("_Color", color);
            }
        }
    }
}
