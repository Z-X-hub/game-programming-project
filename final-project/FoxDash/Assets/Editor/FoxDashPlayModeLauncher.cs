#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace FoxDash.EditorTools
{
    public static class FoxDashPlayModeLauncher
    {
        private const string PlayScenePath = "Assets/Scenes/Play.unity";
        private const string AutoRunFlagName = "FoxDashAutoRun.flag";
        private const string AutoRunPrefsKey = "FoxDash.AutoRunPlayModeRequested";
        private static bool s_RunWhenEditorIsReady;

        [InitializeOnLoadMethod]
        private static void RunWhenRequested()
        {
            EditorApplication.delayCall += () =>
            {
                string flagPath = Path.Combine(Directory.GetParent(Application.dataPath).FullName, "Temp", AutoRunFlagName);
                if (File.Exists(flagPath))
                {
                    File.Delete(flagPath);
                    EditorPrefs.SetBool(AutoRunPrefsKey, true);
                    Debug.Log("Fox Dash auto-run requested.");
                }

                if (!EditorPrefs.GetBool(AutoRunPrefsKey, false))
                {
                    return;
                }

                OpenPlaySceneAndRun();
            };
        }

        [MenuItem("Tools/Fox Dash/Open Play Scene")]
        public static void OpenPlayScene()
        {
            // 统一从主场景启动，避免打开其它资源后直接播放导致缺少管理器对象。
            EditorSceneManager.OpenScene(PlayScenePath);
        }

        [MenuItem("Tools/Fox Dash/Open Play Scene And Run")]
        public static void OpenPlaySceneAndRun()
        {
            s_RunWhenEditorIsReady = true;
            EditorApplication.update -= RunPlayModeWhenEditorIsReady;
            EditorApplication.update += RunPlayModeWhenEditorIsReady;
        }

        private static void RunPlayModeWhenEditorIsReady()
        {
            if (!s_RunWhenEditorIsReady ||
                EditorApplication.isCompiling ||
                EditorApplication.isUpdating ||
                EditorApplication.isPlayingOrWillChangePlaymode)
            {
                return;
            }

            s_RunWhenEditorIsReady = false;
            EditorApplication.update -= RunPlayModeWhenEditorIsReady;
            EditorPrefs.DeleteKey(AutoRunPrefsKey);

            OpenPlayScene();
            EditorApplication.delayCall += () =>
            {
                Debug.Log("Fox Dash entering Play Mode.");
                EditorApplication.isPlaying = true;
            };
        }
    }
}
#endif
