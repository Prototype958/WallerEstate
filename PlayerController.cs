using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform _playerCamera;
    [SerializeField] bool _lockCursor = true;
    [SerializeField] float _hitFindRange;
    [SerializeField] Transform _equipSlot;
    public EnumTypes type;

    [Header("Movement Settings")]
    [SerializeField] bool _canWalk = true;

    [Header("Camera")]
    private CharacterController _characterController;

    [Header("Objects and Equipment")]
    [SerializeField] EquippableObject _currentEquip;
    [SerializeField] InteractableObject _currentInteractable;

    [Header("Input System")]
    [SerializeField] public InputMaster controls;
    private PlayerMovement Movement;

    public bool CanWalk { get { return _canWalk; } set { _canWalk = value; } }

    public static PlayerController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        controls = new InputMaster();
        _playerCamera = Camera.main.transform;

        tag = "Player";
        type = EnumTypes.Player;

        #region INPUT LISTENERS
        // Press E
        controls.Player.DefaultInteract.performed += Interact;

        // Press G
        controls.Player.Drop.performed += DropEquip;
        #endregion

        GameManager.OnGameStateChange += ChangeState;
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }

    private void ChangeState(GameState state)
    {
        switch (state)
        {
            case GameState.Playable:
                EnablePlayerMovement();
                break;
            case GameState.Paused:
            case GameState.Puzzle:
                DisablePlayerMovement();
                break;
        }
    }

    private void EnablePlayerMovement()
    {
        _canWalk = true; // Enables camera look
        controls.Player.Enable(); // Enables movement
    }

    private void DisablePlayerMovement()
    {
        _canWalk = false; // Disables camera look
        controls.Player.Disable(); // Disables movement
        controls.Player.Look.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        Movement = GetComponent<PlayerMovement>();

        if (_lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_canWalk)
        {
            Movement.UpdateMovement();

            // If an item is equipped and Right Mouse Button is pressed
            if (_currentEquip && Input.GetMouseButtonDown(1))
                _currentEquip.ActivateEquippedObject();
        }
    }

    private void FixedUpdate()
    {
        CheckInteraction();
    }

    private void CheckInteraction()
    {
        LayerMask _interactable = LayerMask.GetMask("Interactable");

        RaycastHit hit;

        if (Physics.Raycast(_playerCamera.transform.position, _playerCamera.transform.TransformDirection(Vector3.forward), out hit, _hitFindRange, _interactable))
        {
            if (hit.collider.gameObject.layer == 3 && (_currentInteractable == null || hit.collider.gameObject.GetInstanceID() != _currentInteractable.GetInstanceID()))
            {
                Debug.DrawRay(_playerCamera.transform.position, _playerCamera.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

                hit.collider.TryGetComponent(out _currentInteractable);
            }
            else
                _currentInteractable = null;
        }
        else if (_currentInteractable)
        {
            _currentInteractable = null;
            Debug.DrawRay(_playerCamera.transform.position, _playerCamera.transform.TransformDirection(Vector3.forward) * 5f, Color.white);
        }
    }

    private void DropEquip(InputAction.CallbackContext context)
    {
        if (_currentEquip)
        {
            _currentEquip.Drop();
            _currentEquip = null;
        }
    }

    private void Interact(InputAction.CallbackContext context)
    {
        if (_currentInteractable && !_currentInteractable.Hold)
        {
            if (_currentInteractable.CheckRequiredEquipObject(_currentEquip))
                _currentInteractable.OnInteract();
        }
    }

    public Transform CheckEquipSlot(EquippableObject equip)
    {
        if (_currentEquip)
        {
            //Debug.Log("Something already in equipment slot");
            NotificationManager.Instance.AddMessageToQueue("I'm already holding something");
            return null;
        }
        else
        {
            _currentEquip = equip;
            return _equipSlot;
        }
    }
}