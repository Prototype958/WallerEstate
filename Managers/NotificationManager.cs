using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
     public static NotificationManager Instance { get; private set; }

    [SerializeField] private List<string> _notifications = new List<string>();
    private bool _playing = false;

    [SerializeField] TextMeshProUGUI _notificationZone;
    [SerializeField] Animator _fadeMessageClip;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        _fadeMessageClip = _notificationZone.GetComponent<Animator>();

        _notificationZone.text = null;
        //DisableTextDisplay();
    }

    private void Update()
    {
    }

    private IEnumerator UpdateScreenMessage()
    {
        // Updates the current message to be displayed on screen
        _playing = true;

        while(_notifications.Count > 0)
        {

            _notificationZone.text = _notifications[0];
            _fadeMessageClip.Play("NotificationTextFade", 0, 0);

            while (_fadeMessageClip.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
                yield return null;

            _notifications.RemoveAt(0);
        }
        
        _playing = false;
    } 

    public void AddMessageToQueue(string msg)
    {
        //Global routine called by other systems to add a message to the queue of notifications
        if(!_notifications.Contains(msg))
            _notifications.Add(msg);

        if (!_playing)
            StartCoroutine(UpdateScreenMessage());
    }

    private void EnableTextDisplay()
    {
        _notificationZone.enabled = true;
    }

    private void DisableTextDisplay()
    {
        _notificationZone.enabled = false;
    }
}
