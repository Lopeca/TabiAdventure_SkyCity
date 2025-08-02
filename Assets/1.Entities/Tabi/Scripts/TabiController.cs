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
    private bool jumpCanceled;
    const float JUMP_BUFFER_TIME = 0.1f;
    [SerializeField] float elapsedJumpBufferTime = 0;
    
    private void Reset()
    {
        tabi = GetComponent<Tabi>();
    }

    private void FixedUpdate()
    {
        // FSM으로 옮겨야 함
        //HandleInput();
        HandleJumpBuffer();
    }

    private void HandleJumpBuffer()
    {
        if (jumpBuffer) elapsedJumpBufferTime += Time.fixedDeltaTime;
        if (elapsedJumpBufferTime >= JUMP_BUFFER_TIME)
            jumpBuffer = false;
    }

    private void HandleInput()
    {
        if (inputValue == Vector2.zero)
            tabi.Physics.VelocityX = 0;     // 추후 지형 마찰 계수가 생길 시 수정되어야할 곳
        else
            tabi.Physics.VelocityX = inputValue.x * tabi.TabiSO.MoveSpeed;

        if (jumpBuffer && tabi.Physics.IsGrounded)
        {
            tabi.Physics.VelocityY = tabi.TabiSO.JumpForce;
        }
        else if (JumpCanceledCheck())
        {
            
        }
    }

    private bool JumpCanceledCheck()
    {
        if (jumpCanceled)
        {
            jumpCanceled = false;
            return true;
        }
        return false;
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
            elapsedJumpBufferTime = 0;
        }
        else if (context.canceled)
        {
            jumpBuffer = false;
            jumpCanceled = true;
        }
    }

    public override void OnDash()
    {
        throw new System.NotImplementedException();
    }
}
