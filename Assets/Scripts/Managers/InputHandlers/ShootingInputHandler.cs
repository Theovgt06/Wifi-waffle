using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingInputHandler : InputHandler
{
   [SerializeField] private PlayerSystem playerSystem;
    protected override void RegisterInputActions()
    {
        PlayerInput playerInput = GetPlayerInput();
        if (playerInput != null)
        {
            playerInput.actions["Attack"].performed += OnClickPerformed;
            // Suppression de l'abonnement à canceled
        }
        else
        {
            Debug.LogError("PlayerInput is null in ShootingInputHandler");
        }
    }

    protected override void UnregisterInputActions()
    {
        PlayerInput playerInput = GetPlayerInput();
        if (playerInput != null)
        {
            playerInput.actions["Attack"].performed -= OnClickPerformed;
            // Suppression du désabonnement à canceled
        }
    }

// Uniquement la méthode pour le clic
    private void OnClickPerformed(InputAction.CallbackContext context)
    {
        if (playerSystem != null)
        {
            playerSystem.Shoot();
        }

    }
}



