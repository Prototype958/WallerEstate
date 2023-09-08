
using UnityEngine;

public class PuzzleLoadTest : MonoBehaviour
{
    private void OnEnable()
    {
        // Cursor.lockState = CursorLockMode.None;
        // Cursor.visible = true;
        // PlayerController.Instance.CanWalk = false;
        GameManager.Instance.UpdateGameState(GameState.Puzzle);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.UpdateGameState(GameState.Playable);
            this.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
        // PlayerController.Instance.CanWalk = true;
    }
}
