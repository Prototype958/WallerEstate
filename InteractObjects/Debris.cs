using UnityEngine;
using Cinemachine;

public class Debris : InteractableObject
{
    [SerializeField] private PuzzleLoadTest puzzlePrefab;
    [SerializeField] private CinemachineVirtualCamera puzzleCam;

    private BoxCollider _collider;

    public override void Awake()
    {
        type = EnumTypes.WindowBoards;
        base.Awake();

        _requiredEquip = GameObject.FindAnyObjectByType<Hammer>();
        _collider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.UpdateGameState(GameState.Playable);
            puzzleCam.gameObject.SetActive(false);
        }
    }

    public override bool CheckRequiredEquipObject(EquippableObject equipped)
    {
        if (equipped == _requiredEquip)
        {
            return true;
        }
        else
        {
            NotificationManager.Instance.AddMessageToQueue("I'll need a hammer to remove these");
            return false;
        }
    }

    public override void OnInteract()
    {
        base.OnInteract();

        GameManager.Instance.UpdateGameState(GameState.Puzzle);
        puzzleCam.gameObject.SetActive(true);
        _collider.enabled = false;
    }

    public override void PlayAnimation()
    {
        if (anim)
            anim.Play("Base Layer.BoardClear");
    }

    public override void StopAnimation()
    {
        if (anim)
            anim.Play("Base Layer.Idle");
    }
}
