using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface used by switches and selected objects to activate world mechanisms.
/// It stays simple so the code is easy to explain in a coursework report.
/// </summary>
public interface IWorldActivatable
{
    void Activate();
    void Deactivate();
}

/// <summary>
/// Interface used by selected objects that can rotate by puzzle steps.
/// </summary>
public interface IWorldRotatable
{
    void RotateByStep(int direction);
}

/// <summary>
/// Makes a world object selectable and gives it clear visual feedback.
/// Put this on moving platforms, rotating blocks, or switches the player can control.
/// </summary>
[DisallowMultipleComponent]
public class ControllableObject : MonoBehaviour
{
    [Header("Selection")]
    [SerializeField] private string displayName = "World Object";
    [SerializeField] private bool canBeSelected = true;
    [SerializeField] private GameObject selectionIndicator;

    [Header("Highlight")]
    [SerializeField] private Renderer[] highlightRenderers;
    [SerializeField] private Color selectedColor = new Color(1f, 0.86f, 0.2f, 1f);

    [Header("Optional Targets")]
    [Tooltip("Leave empty to activate components on this same GameObject.")]
    [SerializeField] private MonoBehaviour[] activationTargets;
    [Tooltip("Leave empty to rotate components on this same GameObject.")]
    [SerializeField] private MonoBehaviour[] rotationTargets;

    private readonly List<MaterialColorState> originalColors = new List<MaterialColorState>();
    private bool isSelected;

    public string DisplayName
    {
        get { return displayName; }
    }

    public bool CanBeSelected
    {
        get { return canBeSelected; }
    }

    public bool IsSelected
    {
        get { return isSelected; }
    }

    private void Awake()
    {
        if (highlightRenderers == null || highlightRenderers.Length == 0)
        {
            highlightRenderers = GetComponentsInChildren<Renderer>();
        }

        CacheOriginalColors();

        if (selectionIndicator != null)
        {
            selectionIndicator.SetActive(false);
        }
    }

    public void SetSelected(bool selected)
    {
        isSelected = selected;

        if (selectionIndicator != null)
        {
            selectionIndicator.SetActive(selected);
        }

        ApplyHighlight(selected);
    }

    public void ActivateSelected()
    {
        if (activationTargets != null && activationTargets.Length > 0)
        {
            for (int i = 0; i < activationTargets.Length; i++)
            {
                ActivateTarget(activationTargets[i]);
            }

            return;
        }

        MonoBehaviour[] localComponents = GetComponents<MonoBehaviour>();
        for (int i = 0; i < localComponents.Length; i++)
        {
            ActivateTarget(localComponents[i]);
        }
    }

    public void RotateSelected(int direction)
    {
        if (direction == 0)
        {
            return;
        }

        if (rotationTargets != null && rotationTargets.Length > 0)
        {
            for (int i = 0; i < rotationTargets.Length; i++)
            {
                RotateTarget(rotationTargets[i], direction);
            }

            return;
        }

        MonoBehaviour[] localComponents = GetComponents<MonoBehaviour>();
        for (int i = 0; i < localComponents.Length; i++)
        {
            RotateTarget(localComponents[i], direction);
        }
    }

    private void ActivateTarget(MonoBehaviour target)
    {
        if (target == null || target == this)
        {
            return;
        }

        IWorldActivatable activatable = target as IWorldActivatable;
        if (activatable != null)
        {
            activatable.Activate();
            return;
        }

        target.SendMessage("Activate", SendMessageOptions.DontRequireReceiver);
    }

    private void RotateTarget(MonoBehaviour target, int direction)
    {
        if (target == null || target == this)
        {
            return;
        }

        IWorldRotatable rotatable = target as IWorldRotatable;
        if (rotatable != null)
        {
            rotatable.RotateByStep(direction);
            return;
        }

        target.SendMessage("RotateByStep", direction, SendMessageOptions.DontRequireReceiver);
    }

    private void CacheOriginalColors()
    {
        originalColors.Clear();

        if (highlightRenderers == null)
        {
            return;
        }

        for (int rendererIndex = 0; rendererIndex < highlightRenderers.Length; rendererIndex++)
        {
            Renderer targetRenderer = highlightRenderers[rendererIndex];
            if (targetRenderer == null)
            {
                continue;
            }

            Material[] materials = targetRenderer.materials;
            for (int materialIndex = 0; materialIndex < materials.Length; materialIndex++)
            {
                Material material = materials[materialIndex];
                if (material == null)
                {
                    continue;
                }

                string colorProperty = GetColorProperty(material);
                if (string.IsNullOrEmpty(colorProperty))
                {
                    continue;
                }

                originalColors.Add(new MaterialColorState(material, colorProperty, material.GetColor(colorProperty)));
            }
        }
    }

    private void ApplyHighlight(bool selected)
    {
        for (int i = 0; i < originalColors.Count; i++)
        {
            MaterialColorState colorState = originalColors[i];
            Color color = selected ? selectedColor : colorState.OriginalColor;
            colorState.Material.SetColor(colorState.ColorProperty, color);
        }
    }

    private string GetColorProperty(Material material)
    {
        if (material.HasProperty("_BaseColor"))
        {
            return "_BaseColor";
        }

        if (material.HasProperty("_Color"))
        {
            return "_Color";
        }

        return string.Empty;
    }

    private class MaterialColorState
    {
        public readonly Material Material;
        public readonly string ColorProperty;
        public readonly Color OriginalColor;

        public MaterialColorState(Material material, string colorProperty, Color originalColor)
        {
            Material = material;
            ColorProperty = colorProperty;
            OriginalColor = originalColor;
        }
    }
}
