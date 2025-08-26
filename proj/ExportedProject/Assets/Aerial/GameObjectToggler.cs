using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Utility script for enabling and disabling GameObjects by index
/// </summary>
public class GameObjectToggler : MonoBehaviour
{
    // List of objects to toggle
    [SerializeField]
    private List<GameObject> targetObjects = new List<GameObject>();

    /// <summary>
    /// Disables a GameObject at the specified index
    /// </summary>
    /// <param name="index">Index of the GameObject to disable</param>
    public void DisableByIndex(int index)
    {
        if (IsValidIndex(index))
        {
            targetObjects[index].SetActive(false);
        }
    }

    /// <summary>
    /// Enables a GameObject at the specified index
    /// </summary>
    /// <param name="index">Index of the GameObject to enable</param>
    public void EnableByIndex(int index)
    {
        if (IsValidIndex(index))
        {
            targetObjects[index].SetActive(true);
        }
    }

    /// <summary>
    /// Disables multiple GameObjects by their indices
    /// </summary>
    /// <param name="indices">Array of indices to disable</param>
    public void DisableByIndices(int[] indices)
    {
        foreach (int index in indices)
        {
            DisableByIndex(index);
        }
    }

    /// <summary>
    /// Enables multiple GameObjects by their indices
    /// </summary>
    /// <param name="indices">Array of indices to enable</param>
    public void EnableByIndices(int[] indices)
    {
        foreach (int index in indices)
        {
            EnableByIndex(index);
        }
    }

    /// <summary>
    /// Toggle the active state of a GameObject at the specified index
    /// </summary>
    /// <param name="index">Index of the GameObject to toggle</param>
    public void ToggleByIndex(int index)
    {
        if (IsValidIndex(index))
        {
            targetObjects[index].SetActive(!targetObjects[index].activeSelf);
        }
    }

    /// <summary>
    /// Disables all GameObjects in the list
    /// </summary>
    public void DisableAll()
    {
        for (int i = 0; i < targetObjects.Count; i++)
        {
            targetObjects[i].SetActive(false);
        }
    }

    /// <summary>
    /// Enables all GameObjects in the list
    /// </summary>
    public void EnableAll()
    {
        for (int i = 0; i < targetObjects.Count; i++)
        {
            targetObjects[i].SetActive(true);
        }
    }

    /// <summary>
    /// Checks if the given index is valid for the target objects list
    /// </summary>
    /// <param name="index">Index to validate</param>
    /// <returns>True if index is valid, otherwise false</returns>
    private bool IsValidIndex(int index)
    {
        if (index < 0 || index >= targetObjects.Count)
        {
            Debug.LogWarning($"Index {index} is out of range. Valid range is 0 to {targetObjects.Count - 1}.");
            return false;
        }
        return true;
    }

    /// <summary>
    /// Adds a GameObject to the target list at runtime
    /// </summary>
    /// <param name="obj">GameObject to add</param>
    public void AddTargetObject(GameObject obj)
    {
        if (obj != null && !targetObjects.Contains(obj))
        {
            targetObjects.Add(obj);
        }
    }

    /// <summary>
    /// Removes a GameObject from the target list by reference
    /// </summary>
    /// <param name="obj">GameObject to remove</param>
    public void RemoveTargetObject(GameObject obj)
    {
        if (obj != null)
        {
            targetObjects.Remove(obj);
        }
    }

    /// <summary>
    /// Removes a GameObject from the target list by index
    /// </summary>
    /// <param name="index">Index of the GameObject to remove</param>
    public void RemoveTargetObjectByIndex(int index)
    {
        if (IsValidIndex(index))
        {
            targetObjects.RemoveAt(index);
        }
    }
}