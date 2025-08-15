using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

public class TabiController : StellaController
{
    [SerializeField] private Tabi tabi;
    
    [SerializeField, ReadOnly] private Vector2 inputValue;
    public Vector2 InputValue => inputValue;

	[SerializeField, ReadOnly] private bool dashBuffer;
    private bool jumpBuffer;
    public float jumpStartTime;
    public const float JUMP_MAX_DURATION = 0.22f;
    public bool jumpAscending;

    private bool attackBuffer;
    
	public bool DashBuffer => dashBuffer;
    public bool JumpBuffer => jumpBuffer;
    public bool AttackBuffer => attackBuffer;
    private void Reset()
    {
        tabi = GetComponent<Tabi>();
    }
    
    public override void OnMove(Vector2 value)
    {
        inputValue = value;
    }

    public override void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            jumpBuffer = true;
        }
        else if (context.canceled)
        {
            jumpBuffer = false;
        }
    }

    public override void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
			dashBuffer = true;
		}
		else if (context.canceled)
		{
			dashBuffer = false;
		}
    }

    public override void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            attackBuffer = true;
        }
        else if (context.canceled)
        {
            attackBuffer = false;
        }
    }
}
