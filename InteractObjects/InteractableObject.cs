using System;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public static event Action<EnumTypes> ObjectInteraction;

    public bool Hold = false;
    public Animator anim;

    public EnumTypes type;
    public ToolType tool;

    public EquippableObject _requiredEquip;

    public virtual void Awake()
    {
        gameObject.layer = 3;

        if (GetComponent<Animator>())
            anim = GetComponent<Animator>();
        else
            anim = GetComponentInParent<Animator>();
    }

    public virtual void OnInteract()
    {
        ObjectInteraction?.Invoke(type);
    }

    public virtual bool CheckRequiredEquipObject(EquippableObject equipped) { return true; }

    public virtual void PlayAnimation() { }
    public virtual void StopAnimation() { }

    public virtual void OnTryInteract() { }
}
