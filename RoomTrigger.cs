using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    [SerializeField] private RoomTag _RoomTag;

    public static event Action<RoomTag> OnRoomTagChange;

    public void OnTriggerEnter(Collider other)
    {
        OnRoomTagChange?.Invoke(_RoomTag);
        Debug.Log("entered the " + _RoomTag);
    }
}
