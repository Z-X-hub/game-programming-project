using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles keyboard selection of controllable world objects.
/// This is the main input script for the prototype because the character is not directly controlled.
/// </summary>
public class WorldObjectSelector : MonoBehaviour
{
    [Header("Object List")]
    [SerializeField] private bool findObjectsOnStart = true;
    [SerializeField] private List<ControllableObject> controllableObjects = new List<ControllableObject>();

    [Header("Controls")]
    [SerializeField] private KeyCode previousKey = KeyCode.A;
    [SerializeField] private KeyCode previousAltKey = KeyCode.LeftArrow;
    [SerializeField] private KeyCode nextKey = KeyCode.D;
    [SerializeField] private KeyCode nextAltKey = KeyCode.RightArrow;
    [SerializeField] private KeyCode rotateLeftKey = KeyCode.Q;
    [SerializeField] private KeyCode rotateRightKey = KeyCode.E;
    [SerializeField] private KeyCode activateKey = KeyCode.Space;

    private int selectedIndex = -1;

    public ControllableObject SelectedObject
    {
        get
        {
            if (selectedIndex < 0 || selectedIndex >= controllableObjects.Count)
            {
                return null;
            }

            return controllableObjects[selectedIndex];
        }
    }

    private void Start()
    {
        if (findObjectsOnStart)
        {
            RefreshObjectList();
        }

        SelectIndex(controllableObjects.Count > 0 ? 0 : -1);
    }

    private void Update()
    {
        if (!CanReadInput())
        {
            return;
        }

        if (Input.GetKeyDown(previousKey) || Input.GetKeyDown(previousAltKey))
        {
            SelectPrevious();
        }

        if (Input.GetKeyDown(nextKey) || Input.GetKeyDown(nextAltKey))
        {
            SelectNext();
        }

        if (Input.GetKeyDown(rotateLeftKey))
        {
            RotateSelected(-1);
        }

        if (Input.GetKeyDown(rotateRightKey))
        {
            RotateSelected(1);
        }

        if (Input.GetKeyDown(activateKey))
        {
            ActivateSelected();
        }
    }

    public void RefreshObjectList()
    {
        controllableObjects.Clear();

        ControllableObject[] foundObjects = FindObjectsOfType<ControllableObject>();
        for (int i = 0; i < foundObjects.Length; i++)
        {
            if (foundObjects[i] != null && foundObjects[i].CanBeSelected)
            {
                controllableObjects.Add(foundObjects[i]);
            }
        }

        controllableObjects.Sort(CompareByWorldPosition);
    }

    public void SelectNext()
    {
        RemoveMissingObjects();

        if (controllableObjects.Count == 0)
        {
            SelectIndex(-1);
            return;
        }

        SelectIndex((selectedIndex + 1 + controllableObjects.Count) % controllableObjects.Count);
    }

    public void SelectPrevious()
    {
        RemoveMissingObjects();

        if (controllableObjects.Count == 0)
        {
            SelectIndex(-1);
            return;
        }

        SelectIndex((selectedIndex - 1 + controllableObjects.Count) % controllableObjects.Count);
    }

    public void SelectIndex(int index)
    {
        if (SelectedObject != null)
        {
            SelectedObject.SetSelected(false);
        }

        selectedIndex = IsValidIndex(index) ? index : -1;

        if (SelectedObject != null)
        {
            SelectedObject.SetSelected(true);
            UIManager.InstanceSafeUpdateSelected(SelectedObject.DisplayName);
        }
        else
        {
            UIManager.InstanceSafeUpdateSelected(controllableObjects.Count == 0 ? "No controllable objects" : "None");
        }
    }

    public void ActivateSelected()
    {
        RemoveMissingObjects();

        if (SelectedObject != null)
        {
            SelectedObject.ActivateSelected();
        }
    }

    public void RotateSelected(int direction)
    {
        RemoveMissingObjects();

        if (SelectedObject != null)
        {
            SelectedObject.RotateSelected(direction);
        }
    }

    private bool CanReadInput()
    {
        return GameManager.Instance == null || GameManager.Instance.IsGameplayActive;
    }

    private bool IsValidIndex(int index)
    {
        return index >= 0 && index < controllableObjects.Count;
    }

    private void RemoveMissingObjects()
    {
        for (int i = controllableObjects.Count - 1; i >= 0; i--)
        {
            if (controllableObjects[i] == null || !controllableObjects[i].CanBeSelected)
            {
                controllableObjects.RemoveAt(i);
            }
        }

        if (selectedIndex >= controllableObjects.Count)
        {
            selectedIndex = controllableObjects.Count - 1;
        }
    }

    private int CompareByWorldPosition(ControllableObject first, ControllableObject second)
    {
        if (first == null && second == null)
        {
            return 0;
        }

        if (first == null)
        {
            return 1;
        }

        if (second == null)
        {
            return -1;
        }

        return first.transform.position.x.CompareTo(second.transform.position.x);
    }
}
