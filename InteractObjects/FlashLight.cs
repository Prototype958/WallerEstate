using UnityEngine;

public class FlashLight : EquippableObject
{
    [SerializeField] GameObject _flashLight;
    private AudioSource _activeClick;

    public override void Awake()
    {
        type = EnumTypes.Flashlight;
        _rb = GetComponent<Rigidbody>();
        _baseScale = transform.localScale;
        _activeClick = GetComponent<AudioSource>();

        base.Awake();
    }

    public override void OnInteract()
    {
        base.OnInteract();
        PickUp();
    }

    public override void ActivateEquippedObject()
    {
        // Toggle the flashlight
        _activeClick.Play();
        _flashLight.SetActive(!_flashLight.activeSelf);
    }
}
