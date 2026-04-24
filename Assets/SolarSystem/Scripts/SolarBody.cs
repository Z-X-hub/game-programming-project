using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SolarBody : MonoBehaviour
{
    [Header("Kid Friendly Info")]
    public string displayName = "Solar Body";

    [TextArea(2, 4)]
    public string childFriendlyFact = "Click objects to learn about space.";

    [Header("Camera Focus")]
    public float cameraDistance = 4f;
    public float cameraHeight = 1.2f;

    [Header("Feedback")]
    public AudioClip clickSound;
    public Color highlightColor = new Color(1f, 0.86f, 0.25f, 1f);
    public GameObject selectionMarker;
    public ParticleSystem sparkleBurst;

    private Renderer bodyRenderer;
    private Material runtimeMaterial;
    private Color baseEmissionColor;
    private Vector3 baseScale;
    private float highlightTimeRemaining;
    private bool hasEmissionColor;

    private void Awake()
    {
        baseScale = transform.localScale;
        bodyRenderer = GetComponent<Renderer>();

        if (bodyRenderer != null)
        {
            runtimeMaterial = bodyRenderer.material;
            hasEmissionColor = runtimeMaterial.HasProperty("_EmissionColor");

            if (hasEmissionColor)
            {
                baseEmissionColor = runtimeMaterial.GetColor("_EmissionColor");
            }
        }
    }

    private void Update()
    {
        if (highlightTimeRemaining <= 0f)
        {
            return;
        }

        highlightTimeRemaining -= Time.deltaTime;
        float pulse = 1f + Mathf.Sin(Time.time * 10f) * 0.055f;
        transform.localScale = baseScale * pulse;

        if (highlightTimeRemaining <= 0f)
        {
            ClearHighlight();
        }
    }

    private void OnMouseDown()
    {
        if (SolarSystemController.Instance != null)
        {
            SolarSystemController.Instance.SelectBody(this);
        }
    }

    public void Highlight(float seconds)
    {
        highlightTimeRemaining = seconds;

        if (selectionMarker != null)
        {
            selectionMarker.SetActive(true);
        }

        if (sparkleBurst != null)
        {
            sparkleBurst.Play();
        }

        if (runtimeMaterial != null && hasEmissionColor)
        {
            runtimeMaterial.EnableKeyword("_EMISSION");
            runtimeMaterial.SetColor("_EmissionColor", highlightColor * 1.7f);
        }
    }

    public void ClearHighlight()
    {
        highlightTimeRemaining = 0f;
        transform.localScale = baseScale;

        if (selectionMarker != null)
        {
            selectionMarker.SetActive(false);
        }

        if (runtimeMaterial != null && hasEmissionColor)
        {
            runtimeMaterial.SetColor("_EmissionColor", baseEmissionColor);
        }
    }
}
