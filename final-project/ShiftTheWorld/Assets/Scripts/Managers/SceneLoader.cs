using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Simple scene loading helper for menu buttons.
/// Put this on a menu GameObject and connect public methods to UI Button OnClick events.
/// </summary>
public class SceneLoader : MonoBehaviour
{
    [Header("Scene Names")]
    [SerializeField] private string mainMenuSceneName = "MainMenu";
    [SerializeField] private string levelSelectSceneName = "LevelSelect";
    [SerializeField] private string level01SceneName = "Level01";

    [Header("Menu Keyboard Shortcuts")]
    [SerializeField] private KeyCode cancelKey = KeyCode.Escape;
    [SerializeField] private bool loadMainMenuOnCancel;
    [SerializeField] private bool quitGameOnCancel;

    private void Update()
    {
        if (!Input.GetKeyDown(cancelKey))
        {
            return;
        }

        if (loadMainMenuOnCancel)
        {
            LoadMainMenu();
            return;
        }

        if (quitGameOnCancel)
        {
            QuitGame();
        }
    }

    public void LoadMainMenu()
    {
        LoadScene(mainMenuSceneName);
    }

    public void LoadLevelSelect()
    {
        LoadScene(levelSelectSceneName);
    }

    public void LoadLevel01()
    {
        LoadScene(level01SceneName);
    }

    public void LoadFirstPlayableLevel()
    {
        LoadLevel01();
    }

    // This method can be connected to a Unity Button and given a scene name in the Inspector.
    public void LoadScene(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName) || sceneName.Trim().Length == 0)
        {
            Debug.LogWarning("SceneLoader cannot load an empty scene name.");
            return;
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneByName(string sceneName)
    {
        LoadScene(sceneName);
    }

    public void RestartCurrentScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit requested. This only closes the app in a built player.");
    }
}
