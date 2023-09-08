using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager Instance { get; private set; }

    [SerializeField] Objective currentObjective;
    [SerializeField] List<Objective> remainingObjectives;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        remainingObjectives.Add(new Objective(false, "Lost in the dark", "Locate and pick up the flashlight.", "Find your flashlight", 0, 1, EnumTypes.Flashlight));
        remainingObjectives.Add(new Objective(false, "Quick Snack", "Head to the kitchen", "Locate the Kitchen", 0, 1, RoomTag.Kitchen));
        remainingObjectives.Add(new Objective(false, "Let some light in", "Uncover some of the windows.", "Boards cleared from windows", 0, 4, EnumTypes.WindowBoards));
    }

    public Objective ActivateNextObjective()
    {
        if (remainingObjectives.Count > 0)
        {
            currentObjective = remainingObjectives[0];
            remainingObjectives.RemoveAt(0);

            currentObjective.ActiveObjective = true;

            return currentObjective;
        }
        else
            return null;
    }
}
