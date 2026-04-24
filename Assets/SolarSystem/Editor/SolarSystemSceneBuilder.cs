using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class SolarSystemSceneBuilder
{
    private const string Root = "Assets/SolarSystem";
    private const string ScenePath = Root + "/Scenes/MainScene.unity";

    [MenuItem("Solar System/Create Interactive Scene")]
    public static void BuildSceneForBatch()
    {
        EnsureFolders();
        ConfigureTextureImporters();

        Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        scene.name = "MainScene";

        Texture2D sunTexture = LoadAsset<Texture2D>(Root + "/Textures/SunTexture.jpg");
        Texture2D earthTexture = LoadAsset<Texture2D>(Root + "/Textures/EarthTexture.jpg");
        Texture2D moonTexture = LoadAsset<Texture2D>(Root + "/Textures/MoonTexture.jpg");
        Texture2D marsTexture = LoadAsset<Texture2D>(Root + "/Textures/MarsTexture.jpg");
        Texture2D spaceTexture = LoadAsset<Texture2D>(Root + "/Textures/SpaceTexture.jpg");

        AudioClip gentleSound = LoadAsset<AudioClip>(Root + "/Audio/dronehum.aif");
        AudioClip mysterySound = LoadAsset<AudioClip>(Root + "/Audio/dronedarkandscary.aif");
        AudioClip warmSound = LoadAsset<AudioClip>(Root + "/Audio/burning.aif");

        Material sunMaterial = CreateStandardMaterial(Root + "/Materials/Sun.mat", sunTexture, new Color(1f, 0.9f, 0.55f, 1f), true, new Color(1f, 0.48f, 0.08f, 1f) * 0.9f);
        Material earthMaterial = CreateStandardMaterial(Root + "/Materials/Earth.mat", earthTexture, Color.white, false, Color.black);
        Material moonMaterial = CreateStandardMaterial(Root + "/Materials/Moon.mat", moonTexture, Color.white, false, Color.black);
        Material marsMaterial = CreateStandardMaterial(Root + "/Materials/Mars.mat", marsTexture, Color.white, false, Color.black);
        Material cometMaterial = CreateStandardMaterial(Root + "/Materials/CometIce.mat", null, new Color(0.62f, 0.92f, 1f, 1f), true, new Color(0.2f, 0.75f, 1f, 1f));
        Material orbitMaterial = CreateLineMaterial(Root + "/Materials/OrbitLine.mat", new Color(0.62f, 0.86f, 1f, 0.42f));
        Material selectionMaterial = CreateLineMaterial(Root + "/Materials/SelectionGlow.mat", new Color(1f, 0.86f, 0.18f, 0.9f));
        Material trailMaterial = CreateLineMaterial(Root + "/Materials/CometTrail.mat", new Color(0.45f, 0.9f, 1f, 0.7f));
        Material starMaterial = CreateParticleMaterial(Root + "/Materials/Starfield.mat", new Color(0.9f, 0.96f, 1f, 0.85f));
        Material skyboxMaterial = CreateSkyboxMaterial(Root + "/Materials/SpaceSkybox.mat", spaceTexture);

        RenderSettings.skybox = skyboxMaterial;
        RenderSettings.ambientLight = new Color(0.12f, 0.14f, 0.19f);
        RenderSettings.fog = true;
        RenderSettings.fogColor = new Color(0.008f, 0.011f, 0.02f);
        RenderSettings.fogDensity = 0.0035f;

        GameObject root = new GameObject("Interactive Solar System");
        GameObject environment = new GameObject("Environment");
        environment.transform.SetParent(root.transform);
        GameObject bodiesGroup = new GameObject("Clickable Bodies");
        bodiesGroup.transform.SetParent(root.transform);
        GameObject orbitGroup = new GameObject("Orbit Paths");
        orbitGroup.transform.SetParent(root.transform);

        CreateStarfield(starMaterial, environment.transform);

        GameObject sunLightObject = new GameObject("Sun Point Light");
        Light sunLight = sunLightObject.AddComponent<Light>();
        sunLight.type = LightType.Point;
        sunLight.color = new Color(1f, 0.78f, 0.45f);
        sunLight.intensity = 2.6f;
        sunLight.range = 38f;

        GameObject directionalObject = new GameObject("Soft Fill Directional Light");
        directionalObject.transform.SetParent(environment.transform);
        Light directional = directionalObject.AddComponent<Light>();
        directional.type = LightType.Directional;
        directional.intensity = 0.28f;
        directional.transform.rotation = Quaternion.Euler(42f, -35f, 0f);

        SolarBody sun = CreateBody(
            "Sun",
            new Vector3(0f, 0f, 0f),
            3.1f,
            sunMaterial,
            null,
            0f,
            8f,
            "Sun",
            "The Sun is a huge glowing star. It gives light and warmth to the planets.",
            6.5f,
            2.3f,
            warmSound);
        sun.transform.SetParent(bodiesGroup.transform);
        sunLightObject.transform.SetParent(sun.transform);
        sunLightObject.transform.localPosition = Vector3.zero;

        SolarBody earth = CreateBody(
            "Earth",
            new Vector3(7.2f, 0f, 0f),
            1f,
            earthMaterial,
            sun.transform,
            11f,
            45f,
            "Earth",
            "Earth is our home. It has blue oceans, white clouds, and air we can breathe.",
            3.6f,
            1.15f,
            gentleSound);
        earth.transform.SetParent(bodiesGroup.transform);

        SolarBody moon = CreateBody(
            "Moon",
            new Vector3(9.05f, 0f, 0f),
            0.35f,
            moonMaterial,
            earth.transform,
            64f,
            18f,
            "Moon",
            "The Moon travels around Earth. At night, it reflects sunlight like a quiet mirror.",
            1.55f,
            0.55f,
            gentleSound);
        moon.transform.SetParent(bodiesGroup.transform);

        SolarBody mars = CreateBody(
            "Mars",
            new Vector3(-10.6f, 0f, 0f),
            0.72f,
            marsMaterial,
            sun.transform,
            7f,
            36f,
            "Mars",
            "Mars is called the red planet because rusty dust covers much of its surface.",
            3.1f,
            0.95f,
            mysterySound);
        mars.transform.SetParent(bodiesGroup.transform);

        CreateBodyPresentation(sun, "SUN", new Color(1f, 0.83f, 0.22f, 1f), 2.45f, 1.75f, selectionMaterial, starMaterial);
        CreateBodyPresentation(earth, "EARTH", new Color(0.44f, 0.88f, 1f, 1f), 1.15f, 0.72f, selectionMaterial, starMaterial);
        CreateBodyPresentation(moon, "MOON", new Color(0.92f, 0.94f, 1f, 1f), 0.55f, 0.38f, selectionMaterial, starMaterial);
        CreateBodyPresentation(mars, "MARS", new Color(1f, 0.48f, 0.25f, 1f), 0.9f, 0.55f, selectionMaterial, starMaterial);

        CreateOrbitRing("Earth Orbit", orbitGroup.transform, 7.2f, orbitMaterial);
        CreateOrbitRing("Mars Orbit", orbitGroup.transform, 10.6f, orbitMaterial);
        CreateOrbitRing("Moon Orbit", earth.transform, 1.85f, orbitMaterial);
        GameObject comet = CreateComet(sun.transform, cometMaterial, trailMaterial);
        comet.transform.SetParent(bodiesGroup.transform);

        Camera camera = CreateCamera();
        UIRefs ui = CreateUi();
        SolarSystemController controller = camera.gameObject.AddComponent<SolarSystemController>();
        controller.sceneCamera = camera;
        controller.audioSource = camera.GetComponent<AudioSource>();
        controller.defaultLookTarget = sun.transform;
        controller.infoPanel = ui.infoPanel;
        controller.factText = ui.factText;
        controller.progressText = ui.progressText;
        controller.checklistText = ui.checklistText;
        controller.returnButton = ui.returnButton;

        EnsureEventSystem();
        PlayerSettings.productName = "Interactive Solar System for Kids";

        EditorSceneManager.SaveScene(scene, ScenePath);
        EditorBuildSettings.scenes = new[] { new EditorBuildSettingsScene(ScenePath, true) };
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("Interactive Solar System scene created at " + ScenePath);
    }

    public static void ValidateSceneForBatch()
    {
        EditorSceneManager.OpenScene(ScenePath, OpenSceneMode.Single);

        GameObject sun = RequireObject("Sun");
        GameObject earth = RequireObject("Earth");
        GameObject moon = RequireObject("Moon");
        GameObject mars = RequireObject("Mars");
        GameObject cameraObject = RequireObject("Main Camera");
        RequireObject("Kid Friendly UI");
        RequireObject("Mission Panel");
        RequireObject("Starfield");
        RequireObject("Return Home Button");
        RequireObject("Comet Bonus Prefab");
        RequireObject("EARTH Label");
        RequireObject("MOON Label");

        RequireComponent<SolarBody>(sun, "Sun must be clickable.");
        RequireComponent<SolarBody>(earth, "Earth must be clickable.");
        RequireComponent<SolarBody>(moon, "Moon must be clickable.");
        RequireComponent<SolarBody>(mars, "Mars must be clickable.");
        RequireComponent<OrbitMotion>(earth, "Earth must orbit.");
        RequireComponent<OrbitMotion>(moon, "Moon must orbit.");
        RequireComponent<SolarSystemController>(cameraObject, "Main Camera must control selection and focus.");
        RequireComponent<StaticStarfield>(RequireObject("Starfield"), "Starfield must add visual polish.");

        bool buildSettingsContainsScene = false;
        foreach (EditorBuildSettingsScene buildScene in EditorBuildSettings.scenes)
        {
            if (buildScene.enabled && buildScene.path == ScenePath)
            {
                buildSettingsContainsScene = true;
                break;
            }
        }

        if (!buildSettingsContainsScene)
        {
            throw new System.Exception("MainScene is missing from Build Settings.");
        }

        Debug.Log("Interactive Solar System validation passed.");
    }

    private static void EnsureFolders()
    {
        string[] folders =
        {
            Root + "/Audio",
            Root + "/Documentation",
            Root + "/Editor",
            Root + "/Icons",
            Root + "/Materials",
            Root + "/Prefabs",
            Root + "/Scenes",
            Root + "/Scripts",
            Root + "/Textures"
        };

        foreach (string folder in folders)
        {
            Directory.CreateDirectory(folder);
        }
    }

    private static GameObject RequireObject(string name)
    {
        GameObject found = GameObject.Find(name);
        if (found == null)
        {
            throw new System.Exception("Missing scene object: " + name);
        }

        return found;
    }

    private static T RequireComponent<T>(GameObject target, string message) where T : Component
    {
        T component = target.GetComponent<T>();
        if (component == null)
        {
            throw new System.Exception(message + " Object: " + target.name);
        }

        return component;
    }

    private static void ConfigureTextureImporters()
    {
        string[] texturePaths =
        {
            Root + "/Textures/SunTexture.jpg",
            Root + "/Textures/EarthTexture.jpg",
            Root + "/Textures/MarsTexture.jpg",
            Root + "/Textures/MoonTexture.jpg",
            Root + "/Textures/SpaceTexture.jpg"
        };

        foreach (string path in texturePaths)
        {
            TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
            if (importer == null)
            {
                continue;
            }

            importer.textureType = TextureImporterType.Default;
            importer.wrapMode = TextureWrapMode.Repeat;
            importer.mipmapEnabled = true;
            importer.SaveAndReimport();
        }
    }

    private static T LoadAsset<T>(string path) where T : Object
    {
        AssetDatabase.ImportAsset(path);
        T asset = AssetDatabase.LoadAssetAtPath<T>(path);
        if (asset == null)
        {
            Debug.LogWarning("Missing asset: " + path);
        }

        return asset;
    }

    private static Material CreateStandardMaterial(string path, Texture2D texture, Color color, bool emission, Color emissionColor)
    {
        AssetDatabase.DeleteAsset(path);
        Material material = new Material(Shader.Find("Standard"));
        material.color = color;

        if (texture != null)
        {
            material.mainTexture = texture;
        }

        material.SetFloat("_Glossiness", 0.35f);

        if (emission)
        {
            material.EnableKeyword("_EMISSION");
            material.SetColor("_EmissionColor", emissionColor);
        }

        AssetDatabase.CreateAsset(material, path);
        return material;
    }

    private static Material CreateLineMaterial(string path, Color color)
    {
        AssetDatabase.DeleteAsset(path);
        Shader shader = Shader.Find("Sprites/Default");
        Material material = new Material(shader);
        material.color = color;
        AssetDatabase.CreateAsset(material, path);
        return material;
    }

    private static Material CreateParticleMaterial(string path, Color color)
    {
        AssetDatabase.DeleteAsset(path);
        Shader shader = Shader.Find("Particles/Standard Unlit");
        if (shader == null)
        {
            shader = Shader.Find("Sprites/Default");
        }

        Material material = new Material(shader);
        if (material.HasProperty("_Color"))
        {
            material.SetColor("_Color", color);
        }

        if (material.HasProperty("_Mode"))
        {
            material.SetFloat("_Mode", 2f);
        }

        AssetDatabase.CreateAsset(material, path);
        return material;
    }

    private static Material CreateSkyboxMaterial(string path, Texture2D texture)
    {
        AssetDatabase.DeleteAsset(path);
        Shader shader = Shader.Find("Skybox/Panoramic");
        Material material = new Material(shader);

        if (texture != null)
        {
            material.SetTexture("_MainTex", texture);
        }

        material.SetFloat("_Exposure", 0.42f);
        material.SetFloat("_Rotation", 0f);
        AssetDatabase.CreateAsset(material, path);
        return material;
    }

    private static SolarBody CreateBody(
        string objectName,
        Vector3 position,
        float scale,
        Material material,
        Transform orbitCenter,
        float orbitSpeed,
        float selfRotationSpeed,
        string displayName,
        string fact,
        float cameraDistance,
        float cameraHeight,
        AudioClip clickSound)
    {
        GameObject bodyObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        bodyObject.name = objectName;
        bodyObject.transform.position = position;
        bodyObject.transform.localScale = Vector3.one * scale;
        bodyObject.GetComponent<Renderer>().sharedMaterial = material;

        OrbitMotion orbitMotion = bodyObject.AddComponent<OrbitMotion>();
        orbitMotion.orbitCenter = orbitCenter;
        orbitMotion.orbitSpeedDegrees = orbitSpeed;
        orbitMotion.selfRotationSpeedDegrees = selfRotationSpeed;

        SolarBody body = bodyObject.AddComponent<SolarBody>();
        body.displayName = displayName;
        body.childFriendlyFact = fact;
        body.cameraDistance = cameraDistance;
        body.cameraHeight = cameraHeight;
        body.clickSound = clickSound;

        return body;
    }

    private static void CreateBodyPresentation(SolarBody body, string labelText, Color labelColor, float labelHeight, float markerRadius, Material selectionMaterial, Material sparkleMaterial)
    {
        body.highlightColor = labelColor;
        body.selectionMarker = CreateSelectionMarker(body.transform, markerRadius, selectionMaterial);
        body.sparkleBurst = CreateSparkleBurst(body.transform, markerRadius, labelColor, sparkleMaterial);
        CreateLabel(body.transform, labelText, labelColor, labelHeight);
    }

    private static GameObject CreateSelectionMarker(Transform target, float radius, Material material)
    {
        GameObject marker = new GameObject(target.name + " Selection Glow");
        marker.transform.SetParent(target, false);
        marker.transform.localPosition = Vector3.zero;

        LineRenderer line = marker.AddComponent<LineRenderer>();
        line.useWorldSpace = false;
        line.loop = true;
        line.positionCount = 96;
        line.widthMultiplier = 0.025f;
        line.sharedMaterial = material;
        line.startColor = new Color(1f, 0.86f, 0.18f, 0.9f);
        line.endColor = new Color(1f, 0.86f, 0.18f, 0.9f);

        for (int i = 0; i < line.positionCount; i++)
        {
            float angle = Mathf.PI * 2f * i / line.positionCount;
            line.SetPosition(i, new Vector3(Mathf.Cos(angle) * radius, 0f, Mathf.Sin(angle) * radius));
        }

        marker.SetActive(false);
        return marker;
    }

    private static ParticleSystem CreateSparkleBurst(Transform target, float radius, Color color, Material material)
    {
        GameObject sparkle = new GameObject(target.name + " Sparkle Burst");
        sparkle.transform.SetParent(target, false);
        sparkle.transform.localPosition = Vector3.zero;

        ParticleSystem particles = sparkle.AddComponent<ParticleSystem>();
        ParticleSystem.MainModule main = particles.main;
        main.playOnAwake = false;
        main.loop = false;
        main.duration = 0.65f;
        main.startLifetime = 0.55f;
        main.startSpeed = 0.9f;
        main.startSize = Mathf.Max(0.035f, radius * 0.055f);
        main.startColor = color;
        main.maxParticles = 48;

        ParticleSystem.EmissionModule emission = particles.emission;
        emission.rateOverTime = 0f;
        emission.SetBursts(new[] { new ParticleSystem.Burst(0f, 26) });

        ParticleSystem.ShapeModule shape = particles.shape;
        shape.shapeType = ParticleSystemShapeType.Sphere;
        shape.radius = radius * 0.9f;

        ParticleSystemRenderer renderer = particles.GetComponent<ParticleSystemRenderer>();
        renderer.sharedMaterial = material;
        renderer.renderMode = ParticleSystemRenderMode.Billboard;

        return particles;
    }

    private static void CreateLabel(Transform target, string labelText, Color color, float height)
    {
        GameObject labelRoot = GameObject.Find("Floating Labels");
        if (labelRoot == null)
        {
            labelRoot = new GameObject("Floating Labels");
            labelRoot.transform.SetParent(target.root);
        }

        GameObject label = new GameObject(labelText + " Label");
        label.transform.SetParent(labelRoot.transform);

        TextMesh text = label.AddComponent<TextMesh>();
        text.text = labelText;
        text.anchor = TextAnchor.MiddleCenter;
        text.alignment = TextAlignment.Center;
        text.fontSize = 72;
        text.characterSize = 0.045f;
        text.color = color;

        BillboardLabel billboard = label.AddComponent<BillboardLabel>();
        billboard.target = target;
        billboard.worldOffset = new Vector3(0f, height, 0f);
        billboard.minScale = 0.2f;
        billboard.maxScale = 0.62f;
    }

    private static void CreateStarfield(Material material, Transform parent)
    {
        GameObject starfield = new GameObject("Starfield");
        starfield.transform.SetParent(parent, false);

        ParticleSystem particles = starfield.AddComponent<ParticleSystem>();
        ParticleSystemRenderer renderer = particles.GetComponent<ParticleSystemRenderer>();
        renderer.sharedMaterial = material;
        renderer.renderMode = ParticleSystemRenderMode.Billboard;

        StaticStarfield stars = starfield.AddComponent<StaticStarfield>();
        stars.starCount = 620;
        stars.innerRadius = 34f;
        stars.outerRadius = 76f;
        stars.starSize = 0.15f;
    }

    private static void CreateOrbitRing(string ringName, Transform parent, float radius, Material material)
    {
        GameObject ring = new GameObject(ringName);

        if (parent != null)
        {
            ring.transform.SetParent(parent);
            ring.transform.localPosition = Vector3.zero;
        }

        LineRenderer line = ring.AddComponent<LineRenderer>();
        line.useWorldSpace = false;
        line.loop = true;
        line.positionCount = 144;
        line.widthMultiplier = 0.035f;
        line.sharedMaterial = material;
        line.startColor = new Color(0.62f, 0.86f, 1f, 0.42f);
        line.endColor = new Color(0.62f, 0.86f, 1f, 0.42f);

        for (int i = 0; i < line.positionCount; i++)
        {
            float angle = (Mathf.PI * 2f * i) / line.positionCount;
            line.SetPosition(i, new Vector3(Mathf.Cos(angle) * radius, 0f, Mathf.Sin(angle) * radius));
        }
    }

    private static GameObject CreateComet(Transform sun, Material cometMaterial, Material trailMaterial)
    {
        GameObject comet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        comet.name = "Comet Bonus Prefab";
        comet.transform.position = new Vector3(0f, 0.4f, 12.6f);
        comet.transform.localScale = Vector3.one * 0.25f;
        comet.GetComponent<Renderer>().sharedMaterial = cometMaterial;

        OrbitMotion orbit = comet.AddComponent<OrbitMotion>();
        orbit.orbitCenter = sun;
        orbit.orbitSpeedDegrees = 19f;
        orbit.selfRotationSpeedDegrees = 120f;

        TrailRenderer trail = comet.AddComponent<TrailRenderer>();
        trail.time = 2.7f;
        trail.widthMultiplier = 0.32f;
        trail.sharedMaterial = trailMaterial;
        trail.startColor = new Color(0.45f, 0.92f, 1f, 0.78f);
        trail.endColor = new Color(0.45f, 0.92f, 1f, 0f);

        CometTrail cometTrail = comet.AddComponent<CometTrail>();
        cometTrail.faceTarget = sun;

        AssetDatabase.DeleteAsset(Root + "/Prefabs/CometBonus.prefab");
        PrefabUtility.SaveAsPrefabAssetAndConnect(comet, Root + "/Prefabs/CometBonus.prefab", InteractionMode.AutomatedAction);

        return comet;
    }

    private static Camera CreateCamera()
    {
        GameObject cameraObject = new GameObject("Main Camera");
        cameraObject.tag = "MainCamera";
        cameraObject.transform.position = new Vector3(0f, 6.2f, -18f);
        cameraObject.transform.LookAt(Vector3.zero);

        Camera camera = cameraObject.AddComponent<Camera>();
        camera.clearFlags = CameraClearFlags.Skybox;
        camera.fieldOfView = 52f;
        camera.nearClipPlane = 0.1f;
        camera.farClipPlane = 260f;

        AudioListener listener = cameraObject.AddComponent<AudioListener>();
        listener.enabled = true;

        AudioSource audioSource = cameraObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f;
        audioSource.volume = 0.55f;

        return camera;
    }

    private static UIRefs CreateUi()
    {
        Font font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");

        GameObject canvasObject = new GameObject("Kid Friendly UI");
        Canvas canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObject.AddComponent<GraphicRaycaster>();

        CanvasScaler scaler = canvasObject.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1280f, 720f);
        scaler.matchWidthOrHeight = 0.5f;

        GameObject missionPanel = new GameObject("Mission Panel");
        missionPanel.transform.SetParent(canvasObject.transform, false);
        Image missionImage = missionPanel.AddComponent<Image>();
        missionImage.color = new Color(0.015f, 0.025f, 0.05f, 0.76f);
        RectTransform missionRect = missionPanel.GetComponent<RectTransform>();
        missionRect.anchorMin = new Vector2(0.035f, 0.68f);
        missionRect.anchorMax = new Vector2(0.34f, 0.955f);
        missionRect.offsetMin = Vector2.zero;
        missionRect.offsetMax = Vector2.zero;

        GameObject titleObject = new GameObject("Mission Title");
        titleObject.transform.SetParent(missionPanel.transform, false);
        Text titleText = titleObject.AddComponent<Text>();
        titleText.font = font;
        titleText.fontSize = 25;
        titleText.resizeTextForBestFit = true;
        titleText.resizeTextMinSize = 16;
        titleText.resizeTextMaxSize = 26;
        titleText.alignment = TextAnchor.MiddleLeft;
        titleText.color = new Color(1f, 0.86f, 0.26f, 1f);
        titleText.text = "Mission Control";
        RectTransform titleRect = titleObject.GetComponent<RectTransform>();
        titleRect.anchorMin = new Vector2(0.06f, 0.72f);
        titleRect.anchorMax = new Vector2(0.94f, 0.94f);
        titleRect.offsetMin = Vector2.zero;
        titleRect.offsetMax = Vector2.zero;

        GameObject progressObject = new GameObject("Progress Text");
        progressObject.transform.SetParent(missionPanel.transform, false);
        Text progressText = progressObject.AddComponent<Text>();
        progressText.font = font;
        progressText.fontSize = 20;
        progressText.resizeTextForBestFit = true;
        progressText.resizeTextMinSize = 13;
        progressText.resizeTextMaxSize = 21;
        progressText.alignment = TextAnchor.MiddleLeft;
        progressText.color = new Color(0.82f, 0.95f, 1f, 1f);
        progressText.text = "Visited: 0/4";
        RectTransform progressRect = progressObject.GetComponent<RectTransform>();
        progressRect.anchorMin = new Vector2(0.06f, 0.55f);
        progressRect.anchorMax = new Vector2(0.94f, 0.72f);
        progressRect.offsetMin = Vector2.zero;
        progressRect.offsetMax = Vector2.zero;

        GameObject checklistObject = new GameObject("Checklist Text");
        checklistObject.transform.SetParent(missionPanel.transform, false);
        Text checklistText = checklistObject.AddComponent<Text>();
        checklistText.font = font;
        checklistText.fontSize = 17;
        checklistText.resizeTextForBestFit = true;
        checklistText.resizeTextMinSize = 11;
        checklistText.resizeTextMaxSize = 17;
        checklistText.alignment = TextAnchor.UpperLeft;
        checklistText.color = new Color(0.93f, 0.97f, 1f, 1f);
        checklistText.text = "[ ] Earth discovery\n[ ] Moon discovery\n[ ] Close-up view\n[ ] Sparkle and sound\n[ ] Bonus visit";
        RectTransform checklistRect = checklistObject.GetComponent<RectTransform>();
        checklistRect.anchorMin = new Vector2(0.06f, 0.08f);
        checklistRect.anchorMax = new Vector2(0.94f, 0.54f);
        checklistRect.offsetMin = Vector2.zero;
        checklistRect.offsetMax = Vector2.zero;

        GameObject panelObject = new GameObject("Fact Panel");
        panelObject.transform.SetParent(canvasObject.transform, false);
        Image panelImage = panelObject.AddComponent<Image>();
        panelImage.color = new Color(0.015f, 0.025f, 0.05f, 0.78f);
        RectTransform panelRect = panelObject.GetComponent<RectTransform>();
        panelRect.anchorMin = new Vector2(0.04f, 0.035f);
        panelRect.anchorMax = new Vector2(0.72f, 0.22f);
        panelRect.offsetMin = Vector2.zero;
        panelRect.offsetMax = Vector2.zero;

        GameObject factObject = new GameObject("Fact Text");
        factObject.transform.SetParent(panelObject.transform, false);
        Text factText = factObject.AddComponent<Text>();
        factText.font = font;
        factText.fontSize = 25;
        factText.resizeTextForBestFit = true;
        factText.resizeTextMinSize = 16;
        factText.resizeTextMaxSize = 26;
        factText.alignment = TextAnchor.MiddleLeft;
        factText.color = new Color(0.93f, 0.97f, 1f, 1f);
        factText.text = "Click Earth, Moon, Mars, or the Sun to explore the solar system.";
        RectTransform factRect = factObject.GetComponent<RectTransform>();
        factRect.anchorMin = new Vector2(0.03f, 0.08f);
        factRect.anchorMax = new Vector2(0.97f, 0.92f);
        factRect.offsetMin = Vector2.zero;
        factRect.offsetMax = Vector2.zero;

        GameObject buttonObject = new GameObject("Return Home Button");
        buttonObject.transform.SetParent(canvasObject.transform, false);
        Image buttonImage = buttonObject.AddComponent<Image>();
        buttonImage.color = new Color(1f, 0.78f, 0.18f, 0.92f);
        Button button = buttonObject.AddComponent<Button>();
        button.targetGraphic = buttonImage;
        RectTransform buttonRect = buttonObject.GetComponent<RectTransform>();
        buttonRect.anchorMin = new Vector2(0.76f, 0.055f);
        buttonRect.anchorMax = new Vector2(0.96f, 0.145f);
        buttonRect.offsetMin = Vector2.zero;
        buttonRect.offsetMax = Vector2.zero;

        GameObject buttonTextObject = new GameObject("Text");
        buttonTextObject.transform.SetParent(buttonObject.transform, false);
        Text buttonText = buttonTextObject.AddComponent<Text>();
        buttonText.font = font;
        buttonText.fontSize = 22;
        buttonText.resizeTextForBestFit = true;
        buttonText.resizeTextMinSize = 14;
        buttonText.resizeTextMaxSize = 22;
        buttonText.alignment = TextAnchor.MiddleCenter;
        buttonText.color = new Color(0.07f, 0.05f, 0.01f, 1f);
        buttonText.text = "Return Home";
        RectTransform buttonTextRect = buttonTextObject.GetComponent<RectTransform>();
        buttonTextRect.anchorMin = Vector2.zero;
        buttonTextRect.anchorMax = Vector2.one;
        buttonTextRect.offsetMin = Vector2.zero;
        buttonTextRect.offsetMax = Vector2.zero;

        return new UIRefs
        {
            infoPanel = panelObject,
            factText = factText,
            progressText = progressText,
            checklistText = checklistText,
            returnButton = button
        };
    }

    private static void EnsureEventSystem()
    {
        if (Object.FindObjectOfType<EventSystem>() != null)
        {
            return;
        }

        GameObject eventSystem = new GameObject("EventSystem");
        eventSystem.AddComponent<EventSystem>();
        eventSystem.AddComponent<StandaloneInputModule>();
    }

    private struct UIRefs
    {
        public GameObject infoPanel;
        public Text factText;
        public Text progressText;
        public Text checklistText;
        public Button returnButton;
    }
}
