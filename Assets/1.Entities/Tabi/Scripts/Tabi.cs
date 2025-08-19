using System;
using NaughtyAttributes;
using UnityEngine;

public class Tabi : MonoBehaviour
{
    [Expandable,SerializeField] private TabiSO tabiSO;
    public TabiSO TabiSO => tabiSO;
    [field:SerializeField] public TabiController Controller { get; private set; }
    [field:SerializeField] public EntityPhysics Physics { get; private set; }
    [field:SerializeField] public TabiFSM FSM { get; private set; }
    [field:SerializeField] public Animator Animator { get; private set; }

    public Vector2 LookDirection => transform.eulerAngles.y == 0 ? Vector2.right : Vector2.left;
    private void Reset()
    {
        Physics = GetComponent<EntityPhysics>();
        FSM = GetComponent<TabiFSM>();
    }

    private void OnEnable()
    {
        FSM.Init();
    }

    public void HandleHorizontalInput(bool dash = false)
    {
        float speed = dash ? TabiSO.MoveSpeed * 2 : TabiSO.MoveSpeed;
        Physics.VelocityX = Controller.InputValue.x * speed;
        if(Controller.InputValue.x != 0) Look(Controller.InputValue.x);
    }

    private void Look(float inputValueX)
    {
        transform.eulerAngles = inputValueX > 0 ? new Vector3(0, 0, 0) : new Vector3(0, 180, 0);
    }
    
}
