using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls the level state for the vertical slice: playing, paused, won, or failed.
/// </summary>
public class GameManager : MonoBehaviour
{
    public enum LevelState
    {
        Playing,
        Paused,
        Won,
        Failed
    }

    public static GameManager Instance { get; private set; }

    [Header("Scenes")]
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    [Header("Controls")]
    [SerializeField] private KeyCode restartKey = KeyCode.R;
    [SerializeField] private KeyCode pauseKey = KeyCode.Escape;

    private LevelState currentState = LevelState.Playing;

    public bool IsGameplayActive
    {
        get { return currentState == LevelState.Playing; }
    }

    public bool HasLevelEnded
    {
        get { return currentState == LevelState.Won || currentState == LevelState.Failed; }
    }

    public LevelState CurrentState
    {
        get { return currentState; }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        Time.timeScale = 1f;
    }

    private void Start()
    {
        currentState = LevelState.Playing;
        UIManager.InstanceSafeShowGameplay();
    }

    private void Update()
    {
        if (Input.GetKeyDown(restartKey))
        {
            RestartLevel();
            return;
        }

        if (Input.GetKeyDown(pauseKey))
        {
            HandlePauseKey();
        }
    }

    public void WinLevel(string message)
    {
        if (HasLevelEnded)
        {
            return;
        }

        currentState = LevelState.Won;
        Time.timeScale = 1f;
        UIManager.InstanceSafeShowWin(message);
    }

    public void FailLevel(string reason)
    {
        if (HasLevelEnded)
        {
            return;
        }

        currentState = LevelState.Failed;
        Time.timeScale = 1f;
        UIManager.InstanceSafeShowFail(reason);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        Scene activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.name);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void TogglePause()
    {
        if (HasLevelEnded)
        {
            return;
        }

        if (currentState == LevelState.Paused)
        {
            currentState = LevelState.Playing;
            Time.timeScale = 1f;
            UIManager.InstanceSafeShowGameplay();
        }
        else if (currentState == LevelState.Playing)
        {
            currentState = LevelState.Paused;
            Time.timeScale = 0f;
            UIManager.InstanceSafeShowPause();
        }
    }

    private void HandlePauseKey()
    {
        if (HasLevelEnded)
        {
            LoadMainMenu();
            return;
        }

        TogglePause();
    }
}
