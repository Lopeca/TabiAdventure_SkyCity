using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

public class TabiController : StellaController
{
    [SerializeField] private Tabi tabi;
    
    [SerializeField, ReadOnly] private Vector2 inputValue;
    public Vector2 InputValue => inputValue;

    private bool jumpBuffer;
    public float jumpStartTime;
    public const float JUMP_MAX_DURATION = 0.24f;
    public bool jumpAscending;
    public bool JumpBuffer => jumpBuffer;
    

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

    public override void OnDash()
    {
        throw new System.NotImplementedException();
    }
}
