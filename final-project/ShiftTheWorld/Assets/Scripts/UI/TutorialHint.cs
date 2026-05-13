using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Shows a short first-time instruction panel for Level01.
/// This keeps the tutorial lightweight: no dialogue, no extra systems, just clear UI guidance.
/// </summary>
public class TutorialHint : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject hintPanel;
    [SerializeField] private Text hintText;

    [Header("Hint Copy")]
    [TextArea(3, 6)]
    [SerializeField] private string message =
        "The walker moves by itself.\n" +
        "Your job is to shift the world.\n" +
        "Select highlighted objects and guide the walker to the exit.";

    [Header("Dismiss")]
    [SerializeField] private bool showOnStart = true;
    [SerializeField] private bool autoHide = true;
    [SerializeField] private float autoHideDelay = 7f;
    [SerializeField] private KeyCode dismissKey = KeyCode.Return;
    [SerializeField] private bool hideWhenPlayerUsesWorldControls = true;

    private float visibleTimer;
    private bool isVisible;

    private void Awake()
    {
        if (hintPanel == null)
        {
            hintPanel = gameObject;
        }

        if (hintText == null)
        {
            hintText = GetComponentInChildren<Text>();
        }
    }

    private void Start()
    {
        if (showOnStart)
        {
            ShowHint();
        }
        else
        {
            HideHint();
        }
    }

    private void Update()
    {
        if (!isVisible)
        {
            return;
        }

        visibleTimer += Time.unscaledDeltaTime;

        if (Input.GetKeyDown(dismissKey) || ShouldHideFromWorldControlInput())
        {
            HideHint();
            return;
        }

        if (autoHide && visibleTimer >= autoHideDelay)
        {
            HideHint();
        }
    }

    public void ShowHint()
    {
        visibleTimer = 0f;
        isVisible = true;

        if (hintText != null)
        {
            hintText.text = message;
        }

        if (hintPanel != null)
        {
            hintPanel.SetActive(true);
        }
    }

    public void HideHint()
    {
        isVisible = false;

        if (hintPanel != null)
        {
            hintPanel.SetActive(false);
        }
    }

    private bool ShouldHideFromWorldControlInput()
    {
        if (!hideWhenPlayerUsesWorldControls)
        {
            return false;
        }

        return Input.GetKeyDown(KeyCode.A)
            || Input.GetKeyDown(KeyCode.D)
            || Input.GetKeyDown(KeyCode.LeftArrow)
            || Input.GetKeyDown(KeyCode.RightArrow)
            || Input.GetKeyDown(KeyCode.Q)
            || Input.GetKeyDown(KeyCode.E);
    }
}
