using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform cameraTransform;
    private InputMaster input;

    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _sprintBoost;
    [SerializeField] float _crouchHeight;
    //[SerializeField] private float _jumpHeight = 1.0f;
    [SerializeField] private float _gravityValue = -9.81f;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        input = new InputMaster();
        input.Enable();

        cameraTransform = Camera.main.transform;

        // Press Shift
        input.Player.Sprint.performed += Sprint;
        input.Player.Sprint.canceled += SprintOff;

        // Press Ctrl
        input.Player.Crouch.performed += Crouch;
        input.Player.Crouch.canceled += CrouchOff;
    }

    public void UpdateMovement()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 movement = input.Player.Walk.ReadValue<Vector2>();
        Vector3 move = new Vector3(movement.x, 0f, movement.y);
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        controller.Move(move * Time.deltaTime * _playerSpeed);

        playerVelocity.y += _gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void Sprint(InputAction.CallbackContext context)
    {
        _playerSpeed += _sprintBoost;
    }

    private void SprintOff(InputAction.CallbackContext context)
    {
        _playerSpeed -= _sprintBoost;
    }

    private void Crouch(InputAction.CallbackContext context)
    {
        Vector3 curPos = cameraTransform.localPosition;
        curPos = new Vector3(curPos.x, curPos.y - _crouchHeight, curPos.z);
        cameraTransform.localPosition = curPos;
    }

    private void CrouchOff(InputAction.CallbackContext context)
    {
        Vector3 curPos = cameraTransform.localPosition;
        curPos = new Vector3(curPos.x, curPos.y + _crouchHeight, curPos.z);
        cameraTransform.localPosition = curPos;
    }
}
