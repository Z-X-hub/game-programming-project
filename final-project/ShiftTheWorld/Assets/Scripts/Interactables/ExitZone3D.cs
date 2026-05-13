using UnityEngine;

/// <summary>
/// Trigger zone that wins the level when the auto-walker reaches it.
/// </summary>
[RequireComponent(typeof(Collider))]
public class ExitZone3D : MonoBehaviour
{
    [SerializeField] private string winMessage = "Level Complete";

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
            GameManager.Instance.WinLevel(winMessage);
        }
    }
}
