using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A button or switch that activates linked mechanisms.
/// It can be triggered by the auto-walker entering a trigger collider or by player selection.
/// </summary>
[RequireComponent(typeof(Collider))]
public class SwitchTrigger3D : MonoBehaviour, IWorldActivatable
{
    [Header("Switch Behaviour")]
    [SerializeField] private bool triggerByWalker = true;
    [SerializeField] private bool toggleMode;
    [SerializeField] private bool deactivateWhenWalkerLeaves;
    [SerializeField] private bool triggerOnlyOnce;

    [Header("Targets")]
    [SerializeField] private MonoBehaviour[] activationTargets;

    [Header("Feedback")]
    [SerializeField] private Renderer[] feedbackRenderers;
    [SerializeField] private Color inactiveColor = new Color(0.8f, 0.8f, 0.8f, 1f);
    [SerializeField] private Color activeColor = new Color(0.3f, 1f, 0.45f, 1f);
    [SerializeField] private Vector3 pressedScaleMultiplier = new Vector3(1f, 0.55f, 1f);

    [Header("Events")]
    public UnityEvent OnActivated = new UnityEvent();
    public UnityEvent OnDeactivated = new UnityEvent();

    private Collider triggerCollider;
    private Vector3 originalScale;
    private bool isActive;
    private bool hasTriggered;

    private void Awake()
    {
        triggerCollider = GetComponent<Collider>();
        triggerCollider.isTrigger = true;
        originalScale = transform.localScale;

        if (feedbackRenderers == null || feedbackRenderers.Length == 0)
        {
            feedbackRenderers = GetComponentsInChildren<Renderer>();
        }

        UpdateFeedback();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!triggerByWalker || !IsWalker(other))
        {
            return;
        }

        Activate();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!triggerByWalker || !deactivateWhenWalkerLeaves || !IsWalker(other))
        {
            return;
        }

        Deactivate();
    }

    public void Activate()
    {
        if (triggerOnlyOnce && hasTriggered)
        {
            return;
        }

        if (toggleMode)
        {
            SetActiveState(!isActive);
        }
        else
        {
            SetActiveState(true);
        }

        hasTriggered = true;
    }

    public void Deactivate()
    {
        SetActiveState(false);
    }

    private void SetActiveState(bool active)
    {
        if (isActive == active)
        {
            return;
        }

        isActive = active;
        UpdateFeedback();

        if (isActive)
        {
            SendActivateToTargets();
            if (OnActivated != null)
            {
                OnActivated.Invoke();
            }
        }
        else
        {
            SendDeactivateToTargets();
            if (OnDeactivated != null)
            {
                OnDeactivated.Invoke();
            }
        }
    }

    private void SendActivateToTargets()
    {
        if (activationTargets == null)
        {
            return;
        }

        for (int i = 0; i < activationTargets.Length; i++)
        {
            IWorldActivatable target = activationTargets[i] as IWorldActivatable;
            if (target != null)
            {
                target.Activate();
            }
        }
    }

    private void SendDeactivateToTargets()
    {
        if (activationTargets == null)
        {
            return;
        }

        for (int i = 0; i < activationTargets.Length; i++)
        {
            IWorldActivatable target = activationTargets[i] as IWorldActivatable;
            if (target != null)
            {
                target.Deactivate();
            }
        }
    }

    private void UpdateFeedback()
    {
        Color color = isActive ? activeColor : inactiveColor;

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

        transform.localScale = isActive ? Vector3.Scale(originalScale, pressedScaleMultiplier) : originalScale;
    }

    private bool IsWalker(Collider other)
    {
        return other.GetComponentInParent<AutoWalker3D>() != null;
    }
}
