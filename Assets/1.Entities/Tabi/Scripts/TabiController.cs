using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

public class TabiController : StellaController
{
    [SerializeField] private Tabi tabi;
    
    [SerializeField, ReadOnly] private Vector2 inputValue;
    public Vector2 InputValue => inputValue;

    public bool canControl;

	[SerializeField, ReadOnly] private bool dashBuffer;
    private bool jumpBuffer;
    public float jumpStartTime;
    public const float JUMP_MAX_DURATION = 0.22f;
    public bool jumpAscending;
    
	public bool DashBuffer => dashBuffer;
    public bool JumpBuffer => jumpBuffer;

    public bool horizontalControlLock;
    private void Reset()
    {
        tabi = GetComponent<Tabi>();
    }

    private void Awake()
    {
        canControl = true;
    }

    private void Update()
    {
        // Debug.Log(inputValue);
    }

    public void HandleHorizontalInput(bool dash = false)
    {
        if (!horizontalControlLock)
        {
            float speed = dash ? tabi.TabiSO.MoveSpeed * 2 : tabi.TabiSO.MoveSpeed;
            tabi.Physics.VelocityX = InputValue.x * speed;
        }

        if(canControl && InputValue.x != 0) Look(InputValue.x);
    }

    private void Look(float inputValueX)
    {
        transform.eulerAngles = inputValueX > 0 ? new Vector3(0, 0, 0) : new Vector3(0, 180, 0);
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
}
