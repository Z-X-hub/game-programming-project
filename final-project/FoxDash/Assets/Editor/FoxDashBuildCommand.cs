#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

namespace FoxDash.EditorTools
{
    public static class FoxDashBuildCommand
    {
        private const string PlayScenePath = "Assets/Scenes/Play.unity";

        [MenuItem("Tools/Fox Dash/Build/macOS Standalone")]
        public static void BuildMac()
        {
            FoxDashProjectBranding.Apply();

            string projectRoot = Directory.GetParent(Application.dataPath).FullName;
            string buildDirectory = Path.Combine(projectRoot, "Builds", "FoxDash_Final_Build_Mac");
            string appPath = Path.Combine(buildDirectory, "Fox Dash.app");

            if (Directory.Exists(buildDirectory))
            {
                Directory.Delete(buildDirectory, true);
            }

            Directory.CreateDirectory(buildDirectory);

            var options = new BuildPlayerOptions
            {
                scenes = new[] { PlayScenePath },
                locationPathName = appPath,
                target = BuildTarget.StandaloneOSX,
                options = BuildOptions.None
            };

            var report = BuildPipeline.BuildPlayer(options);
            if (report.summary.result != UnityEditor.Build.Reporting.BuildResult.Succeeded)
            {
                throw new System.Exception($"Fox Dash macOS build failed: {report.summary.result}");
            }

            Debug.Log($"Fox Dash macOS build completed: {appPath}");
        }

        [MenuItem("Tools/Fox Dash/Build/Windows Standalone")]
        public static void BuildWindows()
        {
            FoxDashProjectBranding.Apply();

            string projectRoot = Directory.GetParent(Application.dataPath).FullName;
            string buildDirectory = Path.Combine(projectRoot, "Builds", "FoxDash_Final_Build_Windows");
            string exePath = Path.Combine(buildDirectory, "Fox Dash.exe");

            if (Directory.Exists(buildDirectory))
            {
                Directory.Delete(buildDirectory, true);
            }

            Directory.CreateDirectory(buildDirectory);

            var options = new BuildPlayerOptions
            {
                scenes = new[] { PlayScenePath },
                locationPathName = exePath,
                target = BuildTarget.StandaloneWindows64,
                options = BuildOptions.None
            };

            var report = BuildPipeline.BuildPlayer(options);
            if (report.summary.result != UnityEditor.Build.Reporting.BuildResult.Succeeded)
            {
                throw new System.Exception($"Fox Dash Windows build failed: {report.summary.result}");
            }

            Debug.Log($"Fox Dash Windows build completed: {exePath}");
        }
    }
}
#endif
