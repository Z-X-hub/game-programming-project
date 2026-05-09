using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SimpleSceneActions : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clickClip;

    public void LoadScene(string sceneName)
    {
        PlayClick();
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    public void Show(GameObject target)
    {
        PlayClick();
        if (target != null)
        {
            target.SetActive(true);
        }
    }

    public void Hide(GameObject target)
    {
        PlayClick();
        if (target != null)
        {
            target.SetActive(false);
        }
    }

    public void Quit()
    {
        PlayClick();
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }

    public void PlayClick()
    {
        if (audioSource != null && clickClip != null)
        {
            audioSource.PlayOneShot(clickClip);
        }
    }
}
