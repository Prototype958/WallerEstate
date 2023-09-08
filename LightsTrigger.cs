using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsTrigger : MonoBehaviour
{
    public Light lights;
    private AudioSource scream;

    public void Awake()
    {
        scream = GetComponent<AudioSource>();
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("lights out");
        scream.Play();
        lights.enabled = false;
    }
}
