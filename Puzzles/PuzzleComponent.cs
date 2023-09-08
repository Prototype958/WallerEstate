using System.Collections;
using UnityEngine;

public class PuzzleComponent : MonoBehaviour
{
    private LayerMask _puzzleMask;
    private PuzzleComponentBoard _parent;

    private void Awake()
    {
        _puzzleMask = LayerMask.GetMask("PuzzleComp");
        _parent = transform.parent.gameObject.GetComponent<PuzzleComponentBoard>();
    }

    private void OnMouseDown()
    {

        Debug.Log("click");

        if (Input.GetMouseButton(0))
        {
            Debug.Log("holding");
        }
        else
        {
            Debug.Log("released");
        }
    }
}

// void StartTimer(InputAction.CallbackContext context)
// {
//     if (_currentInteractable && _currentInteractable.Hold)
//     {
//         if (_currentInteractable is Debris)
//         {
//             if (_currentEquip == null || !(_currentEquip is Hammer))
//             {
//                 // Debug.Log("Need hammer to remove these");
//                 NotificationManager.Instance.AddMessageToQueue("I'll need a hammer to remove these");
//             }
//             else
//             {
//                 _timerMaster.SetActive(true);
//                 _timerBar.fillAmount = 0;
//                 _currentInteractable.PlayAnimation();
//                 StartCoroutine(TimerProgress());
//             }
//         }
//     }
// }

// IEnumerator TimerProgress()
// {
//     while (_timerBar.fillAmount < 1 && _currentInteractable != null)
//     {
//         _timerBar.fillAmount = controls.Player.ClearDebris.GetTimeoutCompletionPercentage();

//         yield return null;
//     }

//     if (_timerBar.fillAmount == 1 || _currentInteractable == null)
//     {
//         _timerMaster.SetActive(false);
//         StopCoroutine(TimerProgress());

//         yield return null;
//     }
// }

// void EndTimer(InputAction.CallbackContext context)
// {
//     _timerMaster.SetActive(false);
//     StopCoroutine(TimerProgress());
// }

// void ClearDebris(InputAction.CallbackContext context)
// {
//     if (_currentInteractable && _currentInteractable.Hold)
//     {
//         _currentInteractable.OnInteract();
//     }
// }