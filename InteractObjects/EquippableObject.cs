using System;
using UnityEngine;

public abstract class EquippableObject : InteractableObject
{
    public Rigidbody _rb;
    public Vector3 _baseScale;

    public new virtual void Awake()
    {
        base.Awake();
    }

    public virtual void PickUp()
    {
        Transform _equipSlot = PlayerController.Instance.CheckEquipSlot(this);

        if (_equipSlot != null)
        {
            transform.rotation = _equipSlot.rotation;
            transform.position = _equipSlot.position;
            transform.localScale = _baseScale;
            _rb.useGravity = false;
            _rb.constraints = RigidbodyConstraints.FreezeAll;
            transform.parent = _equipSlot;
        }
    }

    public virtual void Drop()
    {
        transform.parent = null;
        transform.localScale = _baseScale;
        _rb.useGravity = true;
        _rb.constraints = RigidbodyConstraints.None;
    }

    public virtual void ActivateEquippedObject() { }
}
