using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    public List<KeyItems> Inventory = new List<KeyItems>();

    public enum KeyItems
    {
        RustyKey
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    public void AddItem(KeyItems item)
    {
        if(!Inventory.Contains(item))
            Inventory.Add(item);
    }
}
