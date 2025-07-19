using System;
using NaughtyAttributes;
using UnityEngine;

public class TabiController : CharacterController
{
    [SerializeField] private Tabi tabi;
    
    [SerializeField, ReadOnly] private Vector2 inputValue;
    public Vector2 InputValue => inputValue;
    
    private void Reset()
    {
        tabi = GetComponent<Tabi>();
    }

    private void FixedUpdate()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (inputValue == Vector2.zero)
            tabi.Physics.VelocityX = 0;     // 추후 지형 마찰 계수가 생길 시 수정되어야할 곳
        else
            tabi.Physics.VelocityX = inputValue.x * tabi.TabiSO.MoveSpeed;
    }

    public override void OnMove(Vector2 value)
    {
        inputValue = value;
    }

    public override void OnJump()
    {
        throw new System.NotImplementedException();
    }

    public override void OnDash()
    {
        throw new System.NotImplementedException();
    }
}
