using UnityEngine;
using UnityEngine.InputSystem;

public class MoveInputHandler : InputHandler
{
    [SerializeField] private PlayerController playerController;

    private Vector2 moveInput;

    protected override void RegisterInputActions()
    {
        PlayerInput playerInput = GetPlayerInput();
        if (playerInput != null)
        {
            playerInput.actions["Move"].performed += OnMovePerformed;
            playerInput.actions["Move"].canceled += OnMoveCanceled;
            playerInput.actions["Jump"].performed += OnJumpPerformed;
            playerInput.actions["Jump"].canceled += OnJumpCanceled;

        }
        else
        {
            Debug.LogError("PlayerInput is null in MoveInputHandler");
        }
    }

    protected override void UnregisterInputActions()
    {
        PlayerInput playerInput = GetPlayerInput();
        if (playerInput != null)
        {
            playerInput.actions["Move"].performed -= OnMovePerformed;
            playerInput.actions["Move"].canceled -= OnMoveCanceled;
            playerInput.actions["Jump"].performed -= OnJumpPerformed;
            playerInput.actions["Jump"].canceled -= OnJumpCanceled;

        }
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        if (playerController != null)
        {
            playerController.SetMoveDirection(moveInput);
        }
        else
        {
            Debug.LogError("PlayerController non assigné dans MoveInputHandler");
        }
    }


    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
        if (playerController != null)
        {
            playerController.SetMoveDirection(moveInput);
        }
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        if (playerController != null)
        {
            playerController.Jump();
        }
        else
        {
            Debug.LogError("PlayerController non assigné dans MoveInputHandler");
        }
    }

    private void OnJumpCanceled(InputAction.CallbackContext context)
    {
        if (playerController != null)
        {
            playerController.NoJump();
        }
        else
        {
            Debug.LogError("PlayerController non assigné dans MoveInputHandler");
        }
    }


}
