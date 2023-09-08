using UnityEngine;

public class Hammer : EquippableObject
{
    public override void Awake()
    {
        type = EnumTypes.Hammer;
        _rb = GetComponent<Rigidbody>();
        _baseScale = transform.localScale;

        base.Awake();
    }

    public override void OnInteract()
    {
        base.OnInteract();
        PickUp();
    }

    public override void PickUp()
    {
        base.PickUp();

        // Normalize hammer position in equip slot
        transform.localRotation = Quaternion.Euler(new Vector3(80f, -210f, -70));
    }
}
