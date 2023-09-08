using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using TMPro;
using UnityEngine;

public class ObjectiveTracker : MonoBehaviour
{
    private float _startTimer = 5f;
    private Animator _animator;
    [SerializeField] TextMeshProUGUI ObjectiveName;
    [SerializeField] TextMeshProUGUI ObjectiveGoal;

    private Coroutine DelayRoutine;
    private bool _singleGoal;

    private Objective _currentObjective;

    private void Awake()
    {
        // Subscribe to Trigger Events
        InteractableObject.ObjectInteraction += UpdateGoalStatus;
        RoomTrigger.OnRoomTagChange += UpdateGoalStatus;

        ObjectiveName.enabled = false;
        ObjectiveGoal.enabled = false;

        /*if (this.GetComponentInChildren<Animator>())
            _animator = this.GetComponentInChildren<Animator>();

        if(_animator != null)
            _animator.Play("Base Layer.Idle");*/

        // Wait to initially display objective tracker
        DelayRoutine = StartCoroutine(StartDelay(_startTimer));
    }

    private IEnumerator StartDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _currentObjective = ObjectiveManager.Instance.ActivateNextObjective();
        if (_currentObjective != null)
            ActivateObjectiveFrame();
        else
        {
            ObjectiveName.enabled = false;
            ObjectiveGoal.enabled = false;
        }

    }

    private void ActivateObjectiveFrame()
    {
        StopCoroutine(DelayRoutine);

        ObjectiveName.enabled = true;
        ObjectiveGoal.enabled = true;

        if (_currentObjective.maxGoalProgress == 1)
            _singleGoal = true;
        else
            _singleGoal = false;

        ObjectiveName.text = _currentObjective.Name;
        ObjectiveGoal.fontStyle = FontStyles.Normal;

        if (_singleGoal)
            ObjectiveGoal.text = $"{_currentObjective.Goal}";
        else
            ObjectiveGoal.text = $"{_currentObjective.Goal}: {_currentObjective.currentGoalProgress}/{_currentObjective.maxGoalProgress}";


        /*if(_animator != null)
            _animator.Play("Base Layer.NewObjective");*/
    }

    public void EvaluateGoalStatus()
    {
        if (_currentObjective.currentGoalProgress <= _currentObjective.maxGoalProgress)
            if (_singleGoal)
                ObjectiveGoal.text = $"{_currentObjective.Goal}";
            else
                ObjectiveGoal.text = $"{_currentObjective.Goal}: {_currentObjective.currentGoalProgress}/{_currentObjective.maxGoalProgress}";

        if (_currentObjective.currentGoalProgress == _currentObjective.maxGoalProgress)
        {
            ObjectiveGoal.fontStyle = FontStyles.Strikethrough;
            DelayRoutine = StartCoroutine(StartDelay(_startTimer));
        }
    }
    public void UpdateGoalStatus(EnumTypes tag)
    {
        // If interesting tag, update goal progress, then evaluate whether complete
        // and update UI accordingly
        if ((_currentObjective != null) && tag == _currentObjective.InterestingTag)
        {
            _currentObjective.currentGoalProgress++;
            EvaluateGoalStatus();
        }

    }

    public void UpdateGoalStatus(RoomTag tag)
    {
        if ((_currentObjective != null) && tag == _currentObjective.InterestingRoom)
        {
            _currentObjective.currentGoalProgress++;
            EvaluateGoalStatus();
        }

    }

    /*public void UpdateGoalStatus<T>(T tag)
    {
        if (_currentObjective != null)
        { 
            if(tag.GetType() == typeof(EnumTypes))
            {
                if((EnumTypes)Convert.ChangeType(tag, typeof(EnumTypes)) == _currentObjective.InterestingTag)
                {

                }
            }
            else if(tag.GetType() == typeof(RoomTag))
            {
                if((RoomTag)Convert.ChangeType(tag, typeof(RoomTag)) == _currentObjective.InterestingRoom)
                {

                }
            }
            else if(tag.GetType() == typeof(ToolType))
            {
                if((ToolType)Convert.ChangeType(tag, typeof(ToolType)) == _currentObjective.InterestingTool)
                {

                }
            }
        }
    }*/
}
