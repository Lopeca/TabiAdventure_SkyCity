using System;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class StellaController : MonoBehaviour
{
	public event Action OnAttackKeyUp;
	public event Action OnAttackKeyDown;
    public abstract void OnMove(Vector2 value);
    public abstract void OnJump(InputAction.CallbackContext context);
    public abstract void OnDash(InputAction.CallbackContext context);

    public virtual void OnAttack(InputAction.CallbackContext context)
    {
	    if (context.performed)
	    {
		    OnAttackKeyDown?.Invoke();
	    }
	    else if (context.canceled)
	    {
		    OnAttackKeyUp?.Invoke();
	    }
	    
	}


}
