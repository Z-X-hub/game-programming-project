#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace FoxDash.EditorTools
{
    public static class FoxDashProjectBranding
    {
        private const string ProductName = "Fox Dash";
        private const string CompanyName = "Zhu Xuan Studio";
        private const string StandaloneBundleIdentifier = "com.zhuxuan.foxdash";
        private static readonly string[] StandaloneIconPaths =
        {
            "Assets/Sprites/FoxDash/Icons/Icon_1024.png",
            "Assets/Sprites/FoxDash/Icons/Icon_512.png",
            "Assets/Sprites/FoxDash/Icons/Icon_256.png",
            "Assets/Sprites/FoxDash/Icons/Icon_128.png",
            "Assets/Sprites/FoxDash/Icons/Icon_64.png"
        };

        [InitializeOnLoadMethod]
        private static void ApplyBrandingAfterScriptsReload()
        {
            EditorApplication.delayCall += Apply;
        }

        [MenuItem("Tools/Fox Dash/Apply Project Branding")]
        public static void Apply()
        {
            // 统一维护 Unity PlayerSettings，避免项目名散落在多个设置面板里。
            PlayerSettings.productName = ProductName;
            PlayerSettings.companyName = CompanyName;
            PlayerSettings.bundleVersion = "1.0.0";
            PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Standalone, StandaloneBundleIdentifier);
            PlayerSettings.SetIconsForTargetGroup(BuildTargetGroup.Standalone, LoadStandaloneIcons());

            AssetDatabase.SaveAssets();
            Debug.Log("Fox Dash branding applied.");
        }

        private static Texture2D[] LoadStandaloneIcons()
        {
            Texture2D[] icons = new Texture2D[StandaloneIconPaths.Length];
            for (int i = 0; i < StandaloneIconPaths.Length; i++)
            {
                icons[i] = AssetDatabase.LoadAssetAtPath<Texture2D>(StandaloneIconPaths[i]);
                if (icons[i] == null)
                {
                    Debug.LogWarning("Fox Dash icon missing: " + StandaloneIconPaths[i]);
                }
            }

            return icons;
        }
    }
}
#endif
