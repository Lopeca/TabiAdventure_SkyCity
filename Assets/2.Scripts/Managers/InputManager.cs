using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    [SerializeField] StellaController controller;
    
    public void OnMove(InputAction.CallbackContext context)
    {
        controller?.OnMove(context.ReadValue<Vector2>());
    }
    
    public void OnJump(InputAction.CallbackContext context)
    {
        controller?.OnJump(context);
    }
    
    public void OnDash(InputAction.CallbackContext context)
    {
        controller?.OnDash(context);
    }
    
    public void OnAttack(InputAction.CallbackContext context)
    {
        controller?.OnAttack(context);   
    }
}
