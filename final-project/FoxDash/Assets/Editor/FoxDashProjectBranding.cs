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

            AssetDatabase.SaveAssets();
            Debug.Log("Fox Dash branding applied.");
        }
    }
}
#endif
