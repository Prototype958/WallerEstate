using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Door : InteractableObject
{
    //private GameObject _parentPivot;
    [SerializeField] private bool _isLocked;

    private bool _isOpen = false;
    private float _openSpd = 2f;

    private float _rotationAmount = 90f;
    private float _forwardDirection = 0;

    private Vector3 _startRotation;
    private Vector3 _forward;

    private Coroutine _MovingCoroutine;

    private GameObject _player;

    [SerializeField] InventoryManager.KeyItems RequiredItem;

    private void Start()
    {
        type = EnumTypes.Door;
        _startRotation = transform.rotation.eulerAngles;
        _forward = transform.right;

        _player = GameObject.FindGameObjectWithTag("Player");
    }

    public override void OnInteract()
    {
        if (_isLocked)
            CheckPlayerForKey();

        if (!_isOpen && !_isLocked)
            Open(); 
        else if (_isOpen)
            Close();

        if (_isLocked)
            NotificationManager.Instance.AddMessageToQueue("This door appears to be locked. Need to find a key.");
            //Debug.Log("This door appears to be locked. Need to find a key.");

    }

    private IEnumerator MoveDoorOpen(float forwardAmount)
    {
        _isOpen = true;

        Quaternion startRotation = transform.rotation;
        Quaternion endRotation;

        if(forwardAmount >= _forwardDirection)
        {
            endRotation = Quaternion.Euler(new Vector3(0, _startRotation.y + _rotationAmount, 0));
        }
        else
        {
            endRotation = Quaternion.Euler(new Vector3(0, _startRotation.y - _rotationAmount, 0));
        }

        float time = 0;

        while(time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * _openSpd;
        }
    }

    public void Open()
    {
        if (_MovingCoroutine != null)
            StopCoroutine(_MovingCoroutine);

        float dot = Vector3.Dot(-_forward, (_player.transform.position - transform.position).normalized);
        _MovingCoroutine = StartCoroutine(MoveDoorOpen(dot));
    }

    public void Close()
    {
        if(_MovingCoroutine != null)
            StopCoroutine(_MovingCoroutine);

        _MovingCoroutine = StartCoroutine(MoveDoorClosed());
    }

    private IEnumerator MoveDoorClosed()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(_startRotation);

        _isOpen = false;

        float time = 0;

        while(time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * _openSpd;
        }
        
    }

    private void CheckPlayerForKey()
    {
        if (InventoryManager.Instance.Inventory.Contains(RequiredItem))
        {
            _isLocked = false;
            Debug.Log("you unlocked it");
        }
    }
}

