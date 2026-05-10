using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Updates simple prototype UI: objective, selected object, restart hint, pause, win, and fail panels.
/// This uses Unity's built-in UI Text so the project has no extra package dependency.
/// </summary>
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("HUD Text")]
    [SerializeField] private Text objectiveText;
    [SerializeField] private Text selectedObjectText;
    [SerializeField] private Text restartHintText;

    [Header("Panels")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject failPanel;
    [SerializeField] private GameObject pausePanel;

    [Header("Result Text")]
    [SerializeField] private Text winMessageText;
    [SerializeField] private Text failMessageText;

    [Header("Default Copy")]
    [SerializeField] private string objectiveCopy = "Guide the walker to the exit";
    [SerializeField] private string restartHintCopy = "A/D or Left/Right: select  |  Q/E: rotate  |  Space: activate  |  R: restart  |  Esc: pause";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        ShowGameplay();
        UpdateSelectedObject("None");
    }

    public void ShowGameplay()
    {
        SetPanelActive(winPanel, false);
        SetPanelActive(failPanel, false);
        SetPanelActive(pausePanel, false);
        SetText(objectiveText, objectiveCopy);
        SetText(restartHintText, restartHintCopy);
    }

    public void ShowPause()
    {
        SetPanelActive(winPanel, false);
        SetPanelActive(failPanel, false);
        SetPanelActive(pausePanel, true);
    }

    public void ShowWin(string message)
    {
        SetPanelActive(winPanel, true);
        SetPanelActive(failPanel, false);
        SetPanelActive(pausePanel, false);
        SetText(winMessageText, string.IsNullOrEmpty(message) ? "Puzzle solved." : message);
    }

    public void ShowFail(string message)
    {
        SetPanelActive(winPanel, false);
        SetPanelActive(failPanel, true);
        SetPanelActive(pausePanel, false);
        SetText(failMessageText, string.IsNullOrEmpty(message) ? "Try again." : message);
    }

    public void UpdateSelectedObject(string objectName)
    {
        SetText(selectedObjectText, "Selected: " + objectName);
    }

    public static void InstanceSafeShowGameplay()
    {
        if (Instance != null)
        {
            Instance.ShowGameplay();
        }
    }

    public static void InstanceSafeShowPause()
    {
        if (Instance != null)
        {
            Instance.ShowPause();
        }
    }

    public static void InstanceSafeShowWin(string message)
    {
        if (Instance != null)
        {
            Instance.ShowWin(message);
        }
    }

    public static void InstanceSafeShowFail(string message)
    {
        if (Instance != null)
        {
            Instance.ShowFail(message);
        }
    }

    public static void InstanceSafeUpdateSelected(string objectName)
    {
        if (Instance != null)
        {
            Instance.UpdateSelectedObject(objectName);
        }
    }

    private void SetPanelActive(GameObject panel, bool active)
    {
        if (panel != null)
        {
            panel.SetActive(active);
        }
    }

    private void SetText(Text text, string value)
    {
        if (text != null)
        {
            text.text = value;
        }
    }
}
