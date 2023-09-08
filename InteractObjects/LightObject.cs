using UnityEngine;

public class LightObject : InteractableObject
{
    [SerializeField] private Light[] _lights;
    private AudioSource _activeClick;

    public override void Awake()
    {
        type = EnumTypes.LightSwitch;
        _activeClick = GetComponent<AudioSource>();

        // Pre-disable lights
        foreach (Light t in _lights)
        {
            t.enabled = false;
        }
        base.Awake();
    }

    public override void OnInteract()
    {
        _activeClick.Play();
        foreach (Light t in _lights)
        {
            t.enabled = !t.enabled;
        }
    }
}
