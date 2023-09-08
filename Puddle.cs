using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puddle : InteractableObject
{
    public override void Awake()
    {
        Hold = true;
        base.Awake();
    }

    public override void OnInteract()
    {
        base.OnInteract();
        Destroy(this.gameObject);
    }

    public override void OnTryInteract()
    {

        base.OnTryInteract();
    }
}

