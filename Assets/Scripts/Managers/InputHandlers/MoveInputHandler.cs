using UnityEngine;
using UnityEngine.InputSystem;

public class MoveInputHandler : InputHandler
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerJumping playerJumping;

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
        if (playerMovement != null)
        {
            playerMovement.SetMoveDirection(moveInput);
        }
        else
        {
            Debug.LogError("PlayerController non assigné dans MoveInputHandler");
        }
    }


    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
        if (playerMovement != null)
        {
            playerMovement.SetMoveDirection(moveInput);
        }
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        if (playerJumping != null)
        {
            playerJumping.Jump();
        }
        else
        {
            Debug.LogError("PlayerController non assigné dans MoveInputHandler");
        }
    }

    private void OnJumpCanceled(InputAction.CallbackContext context)
    {
        if (playerJumping != null)
        {
            playerJumping.NoJump();
        }
        else
        {
            Debug.LogError("PlayerController non assigné dans MoveInputHandler");
        }
    }


}
