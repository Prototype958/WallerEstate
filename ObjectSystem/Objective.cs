using System;
using UnityEngine;

[Serializable]
public class Objective
{
    public bool ActiveObjective = false;
    
    public string Name;
    public string Description;
    public string Goal;
    public int currentGoalProgress = 0;
    public int maxGoalProgress;
    public EnumTypes InterestingTag;
    public RoomTag InterestingRoom;
    public ToolType InterestingTool;

    public InteractableObject InterestingObject;

    public Objective (bool activeObjective, string name, string description, string goal, int currentGoalProgress, int maxGoalProgress, EnumTypes interestingTag = EnumTypes.None)
    {
        ActiveObjective = activeObjective;
        Name = name;
        Description = description;
        Goal = goal;
        this.currentGoalProgress = currentGoalProgress;
        this.maxGoalProgress = maxGoalProgress;
        InterestingTag = interestingTag;
    }

    public Objective (bool activeObjective, string name, string description, string goal, int currentGoalProgress, int maxGoalProgress, RoomTag interestingRoom = RoomTag.None)
    {
        ActiveObjective = activeObjective;
        Name = name;
        Description = description;
        Goal = goal;
        this.currentGoalProgress = currentGoalProgress;
        this.maxGoalProgress = maxGoalProgress;
        InterestingRoom = interestingRoom;
    }
}