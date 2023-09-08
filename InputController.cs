using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private InputMaster controls = new InputMaster();


    private void Awake()
    {
        // Interact Button - E
        //     Press
        controls.Player.DefaultInteract.performed += Interaction;

        //     Hold
        controls.Player.LongInteract.started += StartLongInteract;
        controls.Player.LongInteract.performed += PerformLongInteract;
        controls.Player.LongInteract.canceled += CancelLongInteract;

        // Drop Held Item - G
        //     Press
        controls.Player.Drop.performed += DropEquip;

        // Sprint - Shift
        //     Press
        controls.Player.Sprint.performed += Sprint;
        controls.Player.Sprint.canceled += SprintOff;

        // Crouch - Ctrl
        //     Press
        controls.Player.Crouch.performed += Crouch;
        controls.Player.Crouch.canceled += CrouchOff;

        //Pasue Menu - Tab
        //     Press
        //controls.Menu.Pause.performed += Pause;

    }

    private void Pause(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    private void CrouchOff(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    private void Crouch(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    private void SprintOff(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    private void Sprint(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    private void DropEquip(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    private void CancelLongInteract(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    private void PerformLongInteract(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    private void StartLongInteract(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void Interaction(InputAction.CallbackContext context)
    {

    }

}
