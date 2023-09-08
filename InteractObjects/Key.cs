using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : InteractableObject
{
    [SerializeField] InventoryManager.KeyItems Item;

    public override void Awake()
    {
        type = EnumTypes.Key;

        base.Awake();
    }

    public override void OnInteract()
    {
        InventoryManager.Instance.AddItem(Item);
        Destroy(gameObject);
        //Debug.Log("player has the key");
        NotificationManager.Instance.AddMessageToQueue("You pick up the Rusty Key.");
        base.OnInteract();
    }
}
