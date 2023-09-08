using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static event Action<GameState> OnGameStateChange;

    public GameState CurrentState;

    [Header("Pause Settings")]
    [SerializeField] private Canvas _gameHUD;
    [SerializeField] private Canvas _pauseScreen;

    public InputMaster controls;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        SetupInput();

        UpdateGameState(GameState.Playable);
    }

    private void Pause(InputAction.CallbackContext context)
    {
        if (PauseController.Instance.TogglePause() == true)
            UpdateGameState(GameState.Paused);
        else
            UpdateGameState(GameState.Playable);
    }

    private void SetupInput()
    {
        controls = new InputMaster();
        controls.Enable();

        controls.Menu.Pause.performed += Pause;
    }

    public void UpdateGameState(GameState newState)
    {
        var prevState = CurrentState;
        switch (newState)
        {
            case GameState.MainMenu:
                break;
            case GameState.Playable:
                HandlePlayState(prevState);
                break;
            case GameState.Paused:
                HandlePausedState(prevState);
                break;
            case GameState.Puzzle:
                HandlePuzzleState(prevState);
                break;
            case GameState.Event:
                break;
            case GameState.End:
                break;
        }

        CurrentState = newState;
        OnGameStateChange?.Invoke(newState);
    }

    private void HandlePlayState(GameState prevState)
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _gameHUD.gameObject.SetActive(true);

        switch (prevState)
        {
            case GameState.MainMenu:
                break;
            case GameState.Paused:
                _pauseScreen.gameObject.SetActive(false);
                Time.timeScale = 1f;
                break;
            case GameState.Puzzle:
                break;
        }
    }

    private void HandlePausedState(GameState prevState)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _gameHUD.gameObject.SetActive(false);
        _pauseScreen.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    private void HandlePuzzleState(GameState prevState)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _gameHUD.gameObject.SetActive(false);
        //PlayerController.Instance.CanWalk = false;
    }
}

public enum GameState
{
    MainMenu,
    Playable,
    Paused,
    Puzzle,
    Event,
    End
}