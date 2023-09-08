using UnityEngine;

public class PauseController : MonoBehaviour
{
    public static PauseController Instance { get; private set; }

    [SerializeField] bool _isPaused = false;
    // [SerializeField] Canvas _gameHUD;
    // [SerializeField] Canvas _pauseScreen;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }
    public bool TogglePause()
    {
        _isPaused = !_isPaused;
        PauseOrResumeGame();

        return _isPaused;
    }

    private void PauseOrResumeGame()
    {
        // if (_isPaused)
        // {
        //     Cursor.lockState = CursorLockMode.None;
        //     Cursor.visible = true;
        //     _gameHUD.gameObject.SetActive(false);
        //     _pauseScreen.gameObject.SetActive(true);
        //     Time.timeScale = 0f;
        // }
        // else
        // {
        //     Cursor.lockState = CursorLockMode.Locked;
        //     Cursor.visible = false;
        //     _gameHUD.gameObject.SetActive(true);
        //     _pauseScreen.gameObject.SetActive(false);
        //     Time.timeScale = 1f;
        // }
    }
}
