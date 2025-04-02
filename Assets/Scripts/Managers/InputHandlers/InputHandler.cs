using UnityEngine;
using UnityEngine.InputSystem;


public abstract class InputHandler : MonoBehaviour
{
   protected virtual void Start()
   {
       // Vérifier que l'InputManager existe et a un PlayerInput valide
       if (InputManager.Instance != null && InputManager.Instance.CurrentPlayerInput != null)
       {
           // Enregistrer les actions
           RegisterInputActions();
       }
       else
       {
        Debug.LogError($"InputHandler in {gameObject.name} can't be Init: InputManager not find or PlayerInput null");


       }
   }
  
   protected virtual void OnEnable()
   {
       // Si déjà démarré, on s'assure que les actions sont enregistrées
       if (InputManager.Instance != null && InputManager.Instance.CurrentPlayerInput != null)
       {
           RegisterInputActions();
       }
   }
  
   protected virtual void OnDisable()
   {
       // Si l'InputManager existe toujours, on désenregistre nos actions
       if (InputManager.Instance != null && InputManager.Instance.CurrentPlayerInput != null)
       {
           UnregisterInputActions();
       }
   }
  
   // Les méthodes abstraites ne changent pas
   protected abstract void RegisterInputActions();
   protected abstract void UnregisterInputActions();
  
   // Helper pour avoir facilement accès au PlayerInput
   protected PlayerInput GetPlayerInput()
   {
       if (InputManager.Instance != null)
       {
           return InputManager.Instance.CurrentPlayerInput;
       }
       return null;
   }
}

