using UnityEngine;

/// <summary>
/// Trigger zone that fails the level when the auto-walker touches it.
/// Use this for pits, spikes, lasers, or red danger blocks.
/// </summary>
[RequireComponent(typeof(Collider))]
public class HazardZone3D : MonoBehaviour
{
    [SerializeField] private string failReason = "The walker hit a hazard.";

    private void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<AutoWalker3D>() == null)
        {
            return;
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.FailLevel(failReason);
        }
    }
}
