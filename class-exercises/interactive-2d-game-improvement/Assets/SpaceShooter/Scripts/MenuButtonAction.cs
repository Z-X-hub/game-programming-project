using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuButtonAction : MonoBehaviour
{
    public enum MenuAction
    {
        LoadScene,
        ShowPanel,
        HidePanel,
        Quit
    }

    public MenuAction action;
    public string sceneName;
    public GameObject targetPanel;
    public SimpleSceneActions sharedActions;

    public void Execute()
    {
        if (sharedActions != null)
        {
            sharedActions.PlayClick();
        }

        switch (action)
        {
            case MenuAction.LoadScene:
                Time.timeScale = 1f;
                SceneManager.LoadScene(sceneName);
                break;
            case MenuAction.ShowPanel:
                if (targetPanel != null)
                {
                    targetPanel.SetActive(true);
                }
                break;
            case MenuAction.HidePanel:
                if (targetPanel != null)
                {
                    targetPanel.SetActive(false);
                }
                break;
            case MenuAction.Quit:
                Application.Quit();
#if UNITY_EDITOR
                EditorApplication.isPlaying = false;
#endif
                break;
        }
    }
}
